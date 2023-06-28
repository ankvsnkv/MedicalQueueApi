using MedicalQueueApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalQueueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsAllowAny")]

    public class TypePagesController : ControllerBase
    {
        private ApplicationContext db;

        const string NO_TYPEPAGES = "В базе данных не найден ни один тип страниц.";

        public TypePagesController(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Метод для получения справочника типов страниц.
        /// </summary>
        //// Аннотация авторизации, при включении требует
        //// http-заголовок с выданным ранее jwt-токеном 
        //// Формат: Authorization : Bearer TOKEN
        [HttpGet("list")]
        [Authorize]
        public IActionResult GetTypePagesList()
        {
            // Получение списка типов страниц из БД
            var entries = db.TypePages.OrderBy(x => x.Id);
            // Проверка наличия хотя бы 1 записи
            if (entries.Count() == 0)
                return NotFound(NO_TYPEPAGES);
            return Ok(entries);
        }
    }
}
