using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Service_API.Application.Services;
using Service_API.DataAccess;
using Service_API.DataAccess.Mapper;
using Service_API.DataAccess.Repository;
using Service_API.Domain.Abstract.IRepository;
using Service_API.Domain.Abstract.IService;
using Service_API.Domain.Model;
using Service_API.Infrastructure.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Уровень логирования (можно изменить на Information или другой)
    .WriteTo.Console() // Логи в консоль
    .WriteTo.File("ServiceLogs/log.txt", rollingInterval: RollingInterval.Day) // Логи в файл
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#pragma warning disable CS0618 // Тип или член устарел
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ServiceValidator>();
});
#pragma warning restore CS0618 // Тип или член устарел


builder.Services.AddDbContext<ServiceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        b => b.MigrationsAssembly("Service_API.DataAccess")));

builder.Services.AddAutoMapper(typeof(DomainProfile));

builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();

builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
builder.Services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Укажите ваш RabbitMQ сервер
    });
});

#pragma warning disable CS0618 // Тип или член устарел
builder.Services.AddMassTransitHostedService();
#pragma warning restore CS0618 // Тип или член устарел

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
