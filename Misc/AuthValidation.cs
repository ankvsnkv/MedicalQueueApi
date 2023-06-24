using MedicalQueueApi.Models;
using MedicalQueueApi.Misc;
using Microsoft.IdentityModel.Tokens;
using MedicalQueueApi.Data;
using Microsoft.AspNetCore.Identity;

namespace MedicalQueueApi.Misc
{
    public class AuthValidation
    {
        /// <summary>
        /// Функция валидации данных пользователя.
        /// </summary>
        public static bool isValid(ApplicationContext db, AuthData authData)
        {
            var login = authData.Login;
            var password = Hasher.Hash(authData.Password);
            return db.Administrators.Any(x => x.Login == login && x.Password == password);
        }
    }
}
