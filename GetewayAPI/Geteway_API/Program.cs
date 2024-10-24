using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// ���������� Ocelot � �������������

var ocelotConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // ������������� ������� ���� ��� ������ ������
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true) // �������� ����� ocelot.json
    .Build();

// ��������� Ocelot � �������������� ��������� ������������
builder.Services.AddOcelot(ocelotConfiguration);
// ���������� ������������ � Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// ��������� middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// �������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

// ���������� Ocelot
await app.UseOcelot();

// ������������� ������������
app.MapControllers();

app.Run();


