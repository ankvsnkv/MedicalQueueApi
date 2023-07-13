using MedicalQueueApi.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MedicalQueueApi.Models;
using Microsoft.EntityFrameworkCore;
using MedicalQueueApi.Misc;
using Microsoft.AspNetCore.Authorization;

namespace MedicalQueueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsAllowAny")]

    public class PagesController : ControllerBase
    {
        private ApplicationContext db;

        const string NO_PAGES = "По данному типу страницы нет ни одной записи.";

        public PagesController(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Метод для получения списка страниц с отбором по типу.
        /// </summary>
        //// Аннотация авторизации, при включении требует
        //// http-заголовок с выданным ранее jwt-токеном 
        //// Формат: Authorization : Bearer TOKEN
        [HttpGet("list")]
        //[Authorize]
        public IActionResult GetList([FromQuery] string? typePage)
        {
            // Получение списка страниц из БД с отбором по типу
            var entries = db.Pages.Include(x => x.TypePage).
                Include(x => x.Display).Where(x => x.TypePage != null && x.TypePage.Name == typePage || typePage == null);
            // Проверка наличия хотя бы 1 записи
            if (entries.Count() == 0)
                return NotFound(NO_PAGES);
            return Ok(entries);
        }


        [HttpPost]
        public IActionResult AddEntry([FromBody] PageRequest request)
        {
            var model = request.page;
            db.Pages.Add(model);
            db.SaveChanges();
            return Ok(model);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult RedactEntry([FromBody] PageRequest request, [FromRoute] int id)
        {
            var entry = db.Pages.Include(x => x.TypePage).Include(x => x.Display).FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NO_PAGES);
            var model = request.page;
            entry.Title = model.Title;
            entry.DateCreate = model.DateCreate;
            entry.TimeDisplay = model.TimeDisplay;
            entry.TypePageId = model.TypePageId;
            entry.DisplayId = model.DisplayId;
            db.Pages.Update(entry);
            db.SaveChanges();
            return Ok(entry);
        }

        //[Authorize(Roles = AUTH_ROLE)]
        [HttpDelete("{id}")]
        public IActionResult DeleteEntry([FromRoute] int id)
        {
            var entry = db.Pages.Include(x => x.TypePage).Include(x => x.Display).FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NO_PAGES);
            db.Pages.Remove(entry);
            db.SaveChanges();
            return Ok(entry);
        }

        [HttpGet("{id}")]
        public IActionResult GetConcrete([FromRoute] int id)
        {
            var entry = db.Pages.Include(x => x.TypePage).Include(x => x.Display).FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NO_PAGES);
            return Ok(entry);
        }

        public class PageRequest
        {
            public Page page { get; set; }
        }
    }
}
