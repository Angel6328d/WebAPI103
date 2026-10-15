
using Microsoft.EntityFrameworkCore;
using WebAPI103.Context;
using WebAPI103.Services;
using WebAPI103.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. REGISTRO DE SERVICIOS
builder.Services.AddControllers();

// Configuración de OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger
builder.Services.AddSwaggerGen();

// Configuración de DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Registro de servicios de usuarios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// 2. CONSTRUCCIÓN DE LA APLICACIÓN
var app = builder.Build();

// 3. CONFIGURACIÓN DEL PIPELINE HTTP
if (app.Environment.IsDevelopment())
{
    // OpenAPI
    app.MapOpenApi();

    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
