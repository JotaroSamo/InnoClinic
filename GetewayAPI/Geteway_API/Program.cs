using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//// Добавление конфигурации JWT
//var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

// Добавление Ocelot с конфигурацией

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


