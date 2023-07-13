using MedicalQueueApi.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MedicalQueueApi.Misc;
using MedicalQueueApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace MedicalQueueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsAllowAny")]
    public class ColorShemesController : ControllerBase
    {
        private ApplicationContext db;
        const string NOT_SCHEME = "Цветовая схема отсутствует.";
        //const string AUTH_ROLE = "admin";

        public ColorShemesController(ApplicationContext context)
        {
            db = context;

        }

        /// <summary>
        /// Метод для получения конкретной цветовй схемы с отбором по типу.
        /// </summary>
        //// Аннотация авторизации, при включении требует
        //// http-заголовок с выданным ранее jwt-токеном 
        //// Формат: Authorization : Bearer TOKEN
        [HttpGet]
        //[Authorize]
        public IActionResult SearchScheme([FromQuery] string? schemeName="")
        {
            // Получение конкретной цветовй схемы из БД с отбором по Имени
            var entry = db.ColorSchemes.FirstOrDefault(x => x.Name.Contains(schemeName??""));
            // Проверка наличия хотя бы 1 записи
            if (entry == null)
                return NotFound(NOT_SCHEME);
            return Ok(entry);
        }

        /// <summary>
        /// Просмотр конкретной цветовой схемы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Authorize]
        public IActionResult ViewingScheme([FromRoute] int id)
        {
            var entry = db.ColorSchemes.FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NOT_SCHEME);
            return Ok(entry);
        }

        /// <summary>
        /// Метод для добавления новой цветовой схемы
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public IActionResult CreateScheme([FromBody] ColorScheme scheme)
        {
            var model = scheme;
            db.ColorSchemes.Add(model);
            db.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Метод для изменения полей сущности в БД
        /// </summary>
        /// <param name="scheme">Экземпляр класса ColorScheme</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult RedactScheme([FromBody] ColorScheme scheme, [FromRoute] int id)
        {
            var entry = db.ColorSchemes.FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NOT_SCHEME);
            entry.Name = scheme.Name;
            entry.Description = scheme.Description;
            entry.CssFileProperty = scheme.CssFileProperty;
            entry.IsActive = scheme.IsActive;
            db.ColorSchemes.Update(entry);
            db.SaveChanges();
            return Ok(entry);
        }
        /// <summary>
        /// Удаление записи из БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteScheme([FromRoute] int id)
        {
            var entry = db.ColorSchemes.FirstOrDefault(x => x.Id == id);
            if (entry == null)
                return NotFound(NOT_SCHEME);
            db.ColorSchemes.Remove(entry);
            db.SaveChanges();
            return Ok(entry);
        }
    }
}
