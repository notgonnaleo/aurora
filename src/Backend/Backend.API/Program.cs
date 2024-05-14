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
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Memberships;
using Backend.Infrastructure.Services.ProductTypes;
using System.ComponentModel;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.SubCategories;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Contact;
using Backend.Infrastructure.Services.Addresses;
using Backend.Infrastructure.Services.Profiles;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Stocks;
using Microsoft.AspNetCore.Mvc;
using Backend.Infrastructure.Services.Orders;

var builder = WebApplication.CreateBuilder(args);

var DevAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Authentication & Authorization
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthorizationService>();

// Products
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductMediaService>();
builder.Services.AddScoped<ProductTypeService>();
builder.Services.AddScoped<ProductVariantService>();

// Category
builder.Services.AddScoped<CategoryService>();

// SubCategories
builder.Services.AddScoped<SubCategoryService>();

// Agent
builder.Services.AddScoped<AgentService>();
builder.Services.AddScoped<AgentTypeService>();

// Memberships
builder.Services.AddScoped<MembershipService>();

// Tenants
builder.Services.AddScoped<TenantService>();

// Stock 
builder.Services.AddScoped<StockService>();

// User
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserContextService>();

// Contact
builder.Services.AddScoped<PhoneService>();
builder.Services.AddScoped<EmailAddressService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ProfileService>();

// Order
builder.Services.AddScoped<OrderService>();

/*
 * HOW TO UPDATE AND GENERATE MIGRATIONS:
 * 1) dotnet ef migrations add "APP-SNAPSHOT" --context AppDbContext --output-dir Migrations/AppDbMigrations
 * 2) dotnet ef database update --context AppDbContext --connection "CONNECTION_STRING"
 */

// Application Context
builder.Services
    .AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("App"),
    x => x.MigrationsAssembly("Backend.Infrastructure")));

// Authentication Context
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Auth"),
    x => x.MigrationsAssembly("Backend.Infrastructure")));

/* Session & Cookies */
builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(60));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for session data
builder.Services.AddMemoryCache();

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

    c.OperationFilter<UserContextValidationFilter>();
});
builder.Services.AddAuthorization();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseSession();

// Run the auth system.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
