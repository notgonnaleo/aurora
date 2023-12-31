using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Categorys;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.SubCategory;
using Backend.Domain.Entities.Taxes.CNAE;
using Backend.Domain.Entities.Taxes.ICMS;
using Backend.Domain.Entities.Taxes.Products.Taxes;
using Backend.Domain.Entities.Taxes.Products.Taxes.Commercial;
using Backend.Domain.Entities.Taxes.Products.Taxes.Tributes;
using Backend.Domain.Entities.Taxes.Products.Taxes.TributeSituationCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("App"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }

    }
}
