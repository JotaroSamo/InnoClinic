using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Infrastructure
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } // Время жизни Access Token в минутах
        public int RefreshTokenExpiresDays { get; set; } // Время жизни Refresh Token в днях
    }
}
