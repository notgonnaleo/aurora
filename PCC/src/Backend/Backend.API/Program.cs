using Backend.Infrastructure.Context;
using Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using Backend.Infrastructure.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Products
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductTypeService>();

// Getting shot in the head seems better than having to deal with this garbage and two-context applications
// I really hate this thing

/*
 * HOW TO UPDATE AND GENERATE MIGRATIONS:
 * Alright, so if you are going to run migrations on this you will need to specify the context when running the 
 * "ef-core migrations" commands, and this not optional, you must do it. 
 * just type --context NameOfYourContext at the end of your dotnet ef migration or database update comnand and boom
 * you got it migration wrote up ready to be applied now.
 */

// engole essa ai pra quem falou que nao tem pq fazer dois bancos separados ze (indireta faat)

// Application Context
builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("LocalAppDb"),
    x => x.MigrationsAssembly("Backend.Infrastructure")));

// Authentication Context
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("LocalAuthDb"),
    x => x.MigrationsAssembly("Backend.Infrastructure")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

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
