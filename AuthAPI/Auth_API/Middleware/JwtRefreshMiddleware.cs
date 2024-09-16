using Auth_API.Domain.Abstract.Repository;
using Auth_API.Domain.Abstract;
using Microsoft.Extensions.Options;
using Auth_API.Infrastructure;

namespace Auth_API.Middleware
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRefreshMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtProvider jwtProvider, IUserRepository userRepository, IOptions<JwtOptions> options)
        {
            // Получаем AccessToken и RefreshToken из куки
            var accessToken = context.Request.Cookies["AccessToken"];
            var refreshToken = context.Request.Cookies["RefreshToken"];

            if (!string.IsNullOrEmpty(accessToken) && jwtProvider.IsTokenExpired(accessToken) && !string.IsNullOrEmpty(refreshToken))
            {
                var email = jwtProvider.GetUserEmailFromExpiredToken(accessToken);
                var user = await userRepository.GetByEmailAsync(email);

                if (user != null && user.RefreshToken == refreshToken)
                {
                    // Генерация нового Access и Refresh токенов
                    var tokens = jwtProvider.GenerateTokens(user);

                    // Обновляем AccessToken в куки
                    context.Response.Cookies.Append("AccessToken", tokens.AccessToken);

                    // Обновляем RefreshToken в куки (если требуется)
                    if (jwtProvider.IsRefreshTokenExpired(refreshToken))
                    {
                        // Если истек — генерируем новый и сохраняем в куки
                        await userRepository.RefreshToken(user.Id, tokens.RefreshToken);
                        context.Response.Cookies.Append("RefreshToken", tokens.RefreshToken);
                    }
                }
            }

            await _next(context);
        }
    }
}
