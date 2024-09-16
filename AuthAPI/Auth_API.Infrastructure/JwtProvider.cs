using Auth_API.Domain.Abstract;
using Auth_API.Domain.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public (string AccessToken, string RefreshToken) GenerateTokens(User user)
        {
            // Генерация Access Token
            var claims = new Claim[]
            {
        new Claim(CustomClaims.UserId, user.Id.ToString()),
        new Claim(CustomClaims.Email, user.Email),
        new Claim(CustomClaims.Role, user.Role.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes)
            ));

            // Генерация Refresh Token
            var refreshToken = GenerateRefreshToken();

            // Возвращаем оба токена
            return (accessToken, refreshToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // Метод для проверки истечения срока действия токена
        public bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken == null)
            {
                return true; // Токен недействителен
            }

            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        // Метод для проверки истечения срока Refresh токена
        public bool IsRefreshTokenExpired(string refreshToken)
        {
          
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(refreshToken);

            if (jwtToken == null)
            {
                return true; // Токен недействителен или не существует
            }

            return jwtToken.ValidTo < DateTime.UtcNow.AddDays(-1);
        }

        // Метод для извлечения Email из истекшего токена
        public string GetUserEmailFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken?.Claims.FirstOrDefault(c => c.Type == CustomClaims.Email)?.Value;
        }
    }
}
