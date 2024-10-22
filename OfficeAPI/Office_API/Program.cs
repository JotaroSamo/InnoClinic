

using Microsoft.EntityFrameworkCore;
using Office_API.Application.Service;
using Office_API.DataAccess;
using Office_API.DataAccess.Mapper;
using Office_API.DataAccess.Repositories;
using Office_API.Domain.Abstract.Repositories;
using Office_API.Domain.Abstract.Service;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("OfficeLog/Log.txt")
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OfficeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        b => b.MigrationsAssembly("Office_API.DataAccess")));
builder.Services.AddScoped<IOfficeRepositories, OfficeRepositories>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddAutoMapper(typeof(DomainProfile));



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
