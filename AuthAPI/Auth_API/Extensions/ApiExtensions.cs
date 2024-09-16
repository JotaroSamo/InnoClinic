using Auth_API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Text;

namespace Auth_API.Extensions
{
    public static class ApiExtensions
    {
        // Метод расширения для добавления аутентификации в коллекцию сервисов
        public static void AddApiAuthentication(
            this IServiceCollection services, 
            IConfiguration configuration)     
        {
            // Получаем настройки для JWT из конфигурационного файла (appsettings.json)
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            // Добавляем JWT аутентификацию в сервисы приложения
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    // Настройки для проверки и валидации JWT токена
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,                  // Отключаем проверку издателя токена (Issuer)
                        ValidateAudience = false,                // Отключаем проверку аудитории токена (Audience)
                        ValidateLifetime = true,                 // Включаем проверку срока действия токена
                        ValidateIssuerSigningKey = true,         // Включаем проверку подписи токена с помощью ключа
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)), // Симметричный ключ для подписи токенов
                         
                    };

                    // Настраиваем обработку событий для JWT
                    options.Events = new JwtBearerEvents
                    {
                        // Обработка события получения сообщения с токеном
                        OnMessageReceived = context =>
                        {
                            // Извлекаем токен из куки "Access-token"
                            context.Token = context.Request.Cookies["AccessToken"];

                            // Возвращаем завершенную задачу (Task), так как обработка завершена
                            return Task.CompletedTask;
                        }
                    };
                    
                });
            services.AddAuthorization(options =>
            {
               
                options.AddPolicy("Patients", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireRole("Patients");
                }
                );
            });
        }
    }

}
