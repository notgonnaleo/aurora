using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.Taxes.CNAE;
using Backend.Domain.Entities.Taxes.ICMS;
using Backend.Domain.Entities.Taxes.Products.Taxes;
using Backend.Domain.Entities.Taxes.Products.Taxes.Commercial;
using Backend.Domain.Entities.Taxes.Products.Taxes.Tributes;
using Backend.Domain.Entities.Taxes.Products.Taxes.TributeSituationCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //calmer-emu-12306.7tt.cockroachlabs.cloud
        //TODO: Review this
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Database=appdb;Port=5432;User ID=postgres;Password=1234;Pooling=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Addd the Postgres Extension for UUID generation
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseSerialColumns();
        }

        // Agents
        public DbSet<Agent> Agents { get; set; }

        // Products
        public DbSet<Product> Products { get; set; }
        //ProductTypes
        public DbSet<ProductType> ProductTypes { get; set; }    
    }
}
