using Document_API.Application.Service;
using Document_API.DataAccess.Repository;
using Document_API.DataAccess;
using Document_API.Domain.Absract.IRepository;
using Document_API.Infrasructure.Validator;
using FluentValidation.AspNetCore;
using static CSharpFunctionalExtensions.Result;
using Document_API.DataAccess.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#pragma warning disable CS0618 // Тип или член устарел
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<DocumentValidator>();
});
#pragma warning restore CS0618 // Тип или член устарел

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<DocumentDbContext>();

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();

builder.Services.AddScoped<IPhotoService, PhotoService>();

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
