using GranDT_Clases.IRepos;
using GranDT_Clases.Models;
using GranDT_Clases.Repositories;
using GranDT_Clases.Servicios;
using GranDT_Clases.Services;
using Scalar.AspNetCore; // Agregamos el using de Scalar

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IEquipoRepository, EquipoRepositoryMemoria>();
builder.Services.AddScoped<EquipoService>();

builder.Services.AddScoped<IJugadorRepository, JugadorRepositoryMemoria>();
builder.Services.AddScoped<JugadorService>();

builder.Services.AddScoped<IPlantillaRepository, PlantillaRepositoryMemoria>();
builder.Services.AddScoped<PlantillaService>();

builder.Services.AddScoped<IPlantillaJugadorRepository, PlantillaJugadorRepositoryMemoria>();
builder.Services.AddScoped<PlantillaJugadorService>();

builder.Services.AddScoped<IPosicionRepository, PosicionRepositoryMemoria>();
builder.Services.AddScoped<PosicionService>();

builder.Services.AddScoped<IPuntuacionRepository, PuntuacionRepositoryMemoria>();
builder.Services.AddScoped<PuntuacionService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryMemoria>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options => 
    {
        options.Title = "API de GranDT";
        options.Theme = ScalarTheme.Default;
    });

    // Agrega esta línea para redirigir la raíz a Scalar:
    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();