using MedicalQueueApi.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MedicalQueueApi.Misc;

namespace MedicalQueueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsAllowAny")]

    public class AdministratorsController : ControllerBase
    {
        private ApplicationContext db;
        private IConfiguration config;
        const string AUTH_FAILED = "Данные авторизации введены неверно.";
        const string AUTH_ROLE = "admin";

        public AdministratorsController(ApplicationContext context, IConfiguration config)
        {
            db = context;
            this.config = config;
        }

        [HttpPost("token")]
        public IActionResult Login([FromBody] AuthData authData)
        {
            var entry = AuthValidation.getAuth(db, authData);
            if (entry == null)
                return BadRequest(AUTH_FAILED);
            var token = TokenBuilder.BuildToken(new string[] { AUTH_ROLE }, config);
            entry.LastAuth = DateTime.Now;
            return Ok(new { token = token });
        }
    }
}