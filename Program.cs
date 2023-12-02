using MongoDB.Driver;
using RecipesAPI.Models;
using RecipesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RecipeDatabaseSettings>(builder.Configuration.GetSection("RecipeDatabase"));  //taken from appsetting.json
builder.Services.AddSingleton<RecipeService>(); //adding a mongodb singleton service

builder.Services.AddControllers();
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
