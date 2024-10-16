using Appointment_API.Application.Service;
using Appointment_API.Consumer;
using Appointment_API.DataAccess;
using Appointment_API.DataAccess.IService;
using Appointment_API.DataAccess.Repository;
using Appointment_API.Domain.Abstract.IService;
using Appointment_API.Infrastructure.Validator;
using FluentValidation.AspNetCore;
using MassTransit;
using MassTransit.Caching;
using Microsoft.EntityFrameworkCore;
using Profile_API.DataAccess.Mapper;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Уровень логирования (можно изменить на Information или другой)
    .WriteTo.Console() // Логи в консоль
    .WriteTo.File("ServiceLogs/log.txt", rollingInterval: RollingInterval.Day) // Логи в файл
    .CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#pragma warning disable CS0618 // Тип или член устарел
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<AppointmentValidator>();
});
#pragma warning restore CS0618 // Тип или член устарел

builder.Services.AddDbContext<AppointmentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
    b => b.MigrationsAssembly("Appointment_API.DataAccess")));

// Регистрация сервиса и репозитория для Appointment
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

// Регистрация сервиса и репозитория для DoctorAppointment
builder.Services.AddScoped<IDoctorAppointmentService, DoctorAppointmentService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

// Регистрация сервиса и репозитория для PatientAppointment
builder.Services.AddScoped<IPatientAppointmentService, PatientAppointmentService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// Регистрация сервиса и репозитория для ServiceAppointment
builder.Services.AddScoped<IServiceAppointmentService, ServiceAppointmentService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

// Регистрация сервиса и репозитория для Result
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddAutoMapper(typeof(DomainProfile));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DoctorConsumer>();

    x.AddConsumer<PatientConsumer>();

    x.AddConsumer<ServiceConsumer>();


    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Тот же хост RabbitMQ

        cfg.ReceiveEndpoint("doctor_queue", e =>
        {
            e.ConfigureConsumer<DoctorConsumer>(context);
        });
        cfg.ReceiveEndpoint("patient_queue", e =>
        {
            e.ConfigureConsumer<PatientConsumer>(context);
        });

        cfg.ReceiveEndpoint("service_queue", e =>
        {
            e.ConfigureConsumer<ServiceConsumer>(context);
        });

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
