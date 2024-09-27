using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Profile_API.Application.Service;
using Profile_API.Consumer;
using Profile_API.DataAccess;
using Profile_API.DataAccess.Mapper;
using Profile_API.DataAccess.Repositories;
using Profile_API.Domain.Abstract.IService;
using Profile_API.Infrastructure.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#pragma warning disable CS0618 // Тип или член устарел
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<AccountValidator>();

    });
#pragma warning restore CS0618 // Тип или член устарел
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        b => b.MigrationsAssembly("Profile_API.DataAccess")));
builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IReceptionistService, ReceptionistService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AccountCreatedConsumer>();
    x.AddConsumer<CreateSpecializationConsumer>();
    x.AddConsumer<UpdateSpecializationConsumer>();
    x.AddConsumer<DeleteSpecializationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // Тот же хост RabbitMQ

        cfg.ReceiveEndpoint("account-created-queue", e =>
        {
            e.ConfigureConsumer<AccountCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("specialization_create_queue", e =>
        {
            e.ConfigureConsumer<CreateSpecializationConsumer>(context);
        });

        cfg.ReceiveEndpoint("specialization_update_queue", e =>
        {
            e.ConfigureConsumer<UpdateSpecializationConsumer>(context);
        });

        cfg.ReceiveEndpoint("specialization_delete_queue", e =>
        {
            e.ConfigureConsumer<DeleteSpecializationConsumer>(context);
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
