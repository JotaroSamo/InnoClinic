using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



var ocelotConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Устанавливает базовый путь для поиска файлов
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true) // Загрузка файла ocelot.json
    .Build();

// Настройка Ocelot с использованием созданной конфигурации
builder.Services.AddOcelot(ocelotConfiguration);
// Добавление контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Включаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

// Подключаем Ocelot
await app.UseOcelot();

// Маршрутизация контроллеров
app.MapControllers();

app.Run();


