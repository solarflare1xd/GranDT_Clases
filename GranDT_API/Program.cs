using GranDT_Clases.IRepos;
using GranDT_Clases.Models;
using GranDT_Clases.Repositories;
using GranDT_Clases.Servicios;
using GranDT_Clases.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEquipoRepository, EquipoRepositoryMemoria>();
builder.Services.AddScoped<EquipoService>();

builder.Services.AddScoped<IJugadorRepository, JugadorRepositoryMemoria>();
builder.Services.AddScoped<JugadorService>();

builder.Services.AddScoped<IPlantillaRepository, PlantillaRepositoryMemoria>();
builder.Services.AddScoped<PlantillaService>();

builder.Services.AddScoped<IPlantillaJugadorRepository, PlantillaJugadorRepositoryMemoria>();
builder.Services.AddScoped<PlantillaJugadorService>(services => new PlantillaJugadorService(
    services.GetRequiredService<IPlantillaJugadorRepository>(),
    services.GetRequiredService<IJugadorRepository>()));

builder.Services.AddScoped<IPosicionRepository, PosicionRepositoryMemoria>();
builder.Services.AddScoped<PosicionService>();

builder.Services.AddScoped<IPuntuacionRepository, PuntuacionRepositoryMemoria>();
builder.Services.AddScoped<PuntuacionService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryMemoria>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();