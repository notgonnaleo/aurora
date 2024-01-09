using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Category;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.SubCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Backend.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public readonly IConfiguration _configuration;

        public AppDbContext()
        {
        }

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

            modelBuilder.Entity<Product>().HasData(
                    new Product 
                    { 
                        TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                        Id = Guid.NewGuid(),
                        Name = "Samsung Galaxy S4",
                        Description = "Produto de teste gerado na migration - Aurora",
                        Active = true,
                        SKU = "202401",
                        TotalWeight = 0,
                        LiquidWeight = 0,
                        Created = null,
                        CreatedBy = null,
                        Value = 100,
                        Updated = null,
                        UpdatedBy = null,
                        ProductTypeId = 3,
                    },
                    new Product
                    {
                        TenantId = Guid.Parse("ae100414-8fbb-4286-839a-5bafc51a84fb"),
                        Id = Guid.NewGuid(),
                        Name = "Motorola Moto E",
                        Description = "Produto de teste gerado na migration - SampleCompany",
                        Active = true,
                        SKU = "202401",
                        TotalWeight = 0,
                        LiquidWeight = 0,
                        Created = null,
                        CreatedBy = null,
                        Value = 100,
                        Updated = null,
                        UpdatedBy = null,
                        ProductTypeId = 3,
                    }
                );

            modelBuilder.Entity<ProductType>().HasData(
                    new ProductType { Id = 1, Name = "Feedstock", Description = "Crafting material", Active = true },
                    new ProductType { Id = 2, Name = "Intermediate Component", Description = "Intermediate Product/Crafting material", Active = true },
                    new ProductType { Id = 3, Name = "Product", Description = "Final Product", Active = true }
                );
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

    }
}
