using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ICTWebAPI.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "indelco favorite";   // ключ для шифрации
        public const int LIFETIME = 180; // время жизни токена - 180 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
