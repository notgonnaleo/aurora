using Backend.Domain.Entities.Authentication.Membership;
using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authorization.Modules;
using Backend.Domain.Entities.Authorization.Roles;
using Backend.Domain.Entities.Authorization.Subscriptions;
using Backend.Domain.Entities.Authorization.UserRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Context
{
    public class AuthDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Auth"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseSerialColumns();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
