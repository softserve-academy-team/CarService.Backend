using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarService.Api.Models
{
    public class AuthOptions
    {
        public string Issuer { get; set; } = "CarServer"; // издатель токена
        public string Audience { get; set; } = "http://localhost:5000/"; // потребитель токена
        public static string Key { get; set; } = "mysupersecret_secretkey!123";   // ключ для шифрации
        public int Lifetime { get; set; } = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}