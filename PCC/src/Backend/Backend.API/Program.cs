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

// Setting-up context for database migrations.
builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("LocalAppDb"),
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
