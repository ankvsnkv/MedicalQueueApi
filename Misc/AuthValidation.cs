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
        public static Administrator? getAuth(ApplicationContext db, AuthData authData)
        {
            var login = authData.Login;
            var password = Hasher.Hash(authData.Password);
            return db.Administrators.FirstOrDefault(x => x.Login == login && x.Password == password);
        }
    }
}
