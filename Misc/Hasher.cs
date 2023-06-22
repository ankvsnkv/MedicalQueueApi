using System.Security.Cryptography;

namespace MedicalQueueApi.Misc
{
    public class Hasher
    {
        public static string Hash(string passwordStr)
        {
            var password = System.Text.Encoding.UTF8.GetBytes(passwordStr);
            var hash = SHA512.HashData(password);
            return Convert.ToBase64String(hash);
        }
    }
}
