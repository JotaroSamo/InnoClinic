using Auth_API.Application.Service;
using Auth_API.DataAccess;
using Auth_API.DataAccess.Mapper;
using Auth_API.DataAccess.Repositories;
using Auth_API.Domain.Abstract;
using Auth_API.Domain.Abstract.Repository;
using Auth_API.Domain.Abstract.Service;
using Auth_API.Extensions;
using Auth_API.Infrastructure;
using Auth_API.Middleware;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("AuthLog/Log.txt")
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AuthDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
    b => b.MigrationsAssembly("Auth_API.DataAccess")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Укажите ваш RabbitMQ сервер
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseMiddleware<JwtRefreshMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
