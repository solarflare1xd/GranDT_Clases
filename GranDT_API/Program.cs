using GranDT_Clases.IRepos;
using GranDT_Clases.Models;
using GranDT_Clases.Repositories;
using GranDT_Clases.Servicios;
using GranDT_Clases.Services;
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
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GranDT API v1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();