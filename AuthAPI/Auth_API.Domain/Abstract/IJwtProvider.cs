using Auth_API.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.Domain.Abstract
{
    public interface IJwtProvider
    {
        (string AccessToken, string RefreshToken) GenerateTokens(User user);
        bool IsTokenExpired(string token);
        string GetUserEmailFromExpiredToken(string token);
        string GenerateRefreshToken();
        bool IsRefreshTokenExpired(string refreshToken);
    }
}
