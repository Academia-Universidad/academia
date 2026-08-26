using Microsoft.EntityFrameworkCore;
using PorgramacionV.Repositories;
using ProgramacionV.Data;
using ProgramacionV.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContexto>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProgramaRepository>(); 
builder.Services.AddScoped<EstudiantesRepository>(); 

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

//Habilitar Scalar
app.MapScalarApiReference(options =>
{
    options.WithTitle("Programacion V Gestion Academica API");
});


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
