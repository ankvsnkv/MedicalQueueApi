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

    public class PagesController : ControllerBase {
        private ApplicationContext db;

        const string NO_PAGES = "По данному типу страницы нет ни одной записи.";

        public PagesController(ApplicationContext context) {
            db = context;
        }

        /// <summary>
        /// Метод для получения списка страниц с отбором по типу.
        /// </summary>
        //// Аннотация авторизации, при включении требует
        //// http-заголовок с выданным ранее jwt-токеном 
        //// Формат: Authorization : Bearer TOKEN
        [HttpGet("list")]
        [Authorize]
        public IActionResult GetList([FromQuery] string typePage) {
            // Получение списка страниц из БД с отбором по типу
            var entries = db.Pages.Include(x => x.TypePage).
                Include(x => x.Display).Where(x => x.TypePage.Name == typePage);
            // Проверка наличия хотя бы 1 записи
            if (entries.Count() == 0)
                return NotFound(NO_PAGES);
            return Ok(entries);
        }
    }
}
