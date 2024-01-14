using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.SubCategories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Mocks.AppDbMocks
{
    /// <summary>
    /// Application Database Mock/Default Data Generator
    /// </summary>
    public static class AppDbMockDataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedCategories(modelBuilder);
            SeedSubCategories(modelBuilder);
            SeedProducts(modelBuilder);
            SeedProductTypes(modelBuilder);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category
                    {
                        TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                        CategoryId = Guid.Parse("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                        CategoryName = "Eletronic",
                        Active = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        UpdatedBy = null,
                        CreatedBy = null,
                    }
                );
        }

        private static void SeedSubCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>().HasData(
                    new SubCategory 
                    {
                        TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                        SubCategoryId = Guid.Parse("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                        SubCategoryName = "Smartphone",
                        CategoryId = Guid.Parse("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                        Active = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        UpdatedBy = null,
                        CreatedBy = null,
                    }
                );
        }

        private static void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    Id = Guid.NewGuid(),
                    Name = "Samsung Galaxy S4",
                    Description = "Produto de teste gerado na migration - Aurora",
                    GTIN = "012345678910111213",
                    Active = true,
                    SKU = "202401",
                    TotalWeight = 0.13,
                    LiquidWeight = 0.13,
                    Created = DateTime.UtcNow,
                    CreatedBy = null,
                    Value = 604.99,
                    Updated = null,
                    UpdatedBy = null,
                    ProductTypeId = 3,
                    CategoryId = Guid.Parse("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                    SubCategoryId = Guid.Parse("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6")
                },
                new Product
                {
                    TenantId = Guid.Parse("ae100414-8fbb-4286-839a-5bafc51a84fb"),
                    Id = Guid.NewGuid(),
                    Name = "Motorola Moto E",
                    Description = "Produto de teste gerado na migration - SampleCompany",
                    GTIN = "012345678910111213",
                    Active = true,
                    SKU = "202401",
                    TotalWeight = 0,
                    LiquidWeight = 0,
                    Created = DateTime.UtcNow,
                    CreatedBy = null,
                    Value = 100,
                    Updated = null,
                    UpdatedBy = null,
                    ProductTypeId = 3,
                    CategoryId = null
                }
            );
        }

        private static void SeedProductTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Feedstock", Description = "Crafting material", Active = true },
                new ProductType { Id = 2, Name = "Intermediate Component", Description = "Intermediate Product/Crafting material", Active = true },
                new ProductType { Id = 3, Name = "Product", Description = "Final Product", Active = true }
            );
        }
    }
}
