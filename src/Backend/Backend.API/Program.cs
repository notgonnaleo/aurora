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

var builder = WebApplication.CreateBuilder(args);

var DevAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
// Authentication & Authorization
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthorizationService>();

// Products
builder.Services.AddScoped<ProductService>();

// Memberships
builder.Services.AddScoped<MembershipService>();

// Tenants
builder.Services.AddScoped<TenantService>();

// User
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserContextService>();
// Getting shot in the head seems to be the solution for all my problems
// I really hate myself

/*
 * HOW TO UPDATE AND GENERATE MIGRATIONS:
 * Alright, so if you are going to run migrations on this you will need to specify the context when running the 
 * "ef-core migrations" commands, and this not optional, you must do it. 
 * just type --context NameOfYourContext at the end of your dotnet ef migration or database update comnand and boom
 * you got it migration wrote up ready to be applied now.
 * dotnet ef migrations add coolname --context ContextName --output-dir FolderContext
 */

// Application Context
builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("LocalAppDb"),
    x => x.MigrationsAssembly("Backend.Infrastructure")));

// Authentication Context
builder.Services.AddDbContext<AuthDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("LocalAuthDb"),
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DevAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7288", "http://localhost:5012").AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(DevAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseSession();

// Run the auth system.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
