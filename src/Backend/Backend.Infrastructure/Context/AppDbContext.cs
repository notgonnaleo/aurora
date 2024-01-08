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
            => optionsBuilder.UseNpgsql("Server=localhost;Database=appdb;User ID=postgres;Password=1234;Pooling=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseSerialColumns();

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
