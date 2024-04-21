using Backend.Domain.Entities.Addresses;
using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Contacts;
using Backend.Domain.Entities.Orders;
using Backend.Domain.Entities.Payments;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.Profiles;
using Backend.Domain.Entities.Stocks;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Mocks.AppDbMocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.UseSerialColumns();
            AppDbMockDataSeeder.Seed(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductMedia> ProductMedia { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentType> AgentTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Profile> Profiles { get; set; }

    }
}
