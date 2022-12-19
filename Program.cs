using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using GuacAPI.Context;
using GuacAPI.Interface;
using GuacAPI.Repositories;
using GuacAPI.ExtensionMethods;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Db context
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Instancie la class DefaultProductRepository à chaque fois qu'il rencontre une Interface IProductRepository (interface ne s'intanscie pas toute seule)
// builder.Services.AddScoped<IProductRepository, DefaultProductRepository>();

builder.Services.AddInjections(); //Inject all injection depandencies

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

