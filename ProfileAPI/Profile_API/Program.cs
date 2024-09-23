using FluentValidation.AspNetCore;
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
