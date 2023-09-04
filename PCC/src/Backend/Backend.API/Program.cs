using Backend.Infrastructure.Context;
using Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using Backend.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Tenants;
using Backend.Infrastructure.Services.Users;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Authentication
builder.Services.AddScoped<AuthenticationService>();

// Products
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductTypeService>();

// Tenants
builder.Services.AddScoped<TenantService>();

// User
builder.Services.AddScoped<UserService>();

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

/* Authentication & Authorization Middleware section */
// Authenticate
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => 
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWTKeySettings:SecretKey").Value)),
        ValidateIssuerSigningKey = true
    };
});

// Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

// Authorize
builder.Services.AddAuthorization();

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

// Run the auth system.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
