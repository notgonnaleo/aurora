using Backend.Domain.Entities.Addresses;
using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Contacts;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.ProductTypes;
using Backend.Domain.Entities.Profiles;
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
            SeedAgentTypes(modelBuilder);

            SeedAgent(modelBuilder);
            SeedPhone(modelBuilder);
            SeedEmail(modelBuilder);
            SeedAddress(modelBuilder);
            SeedProfile(modelBuilder);
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
                    ProductId = Guid.NewGuid(),
                    Name = "Samsung Galaxy S4",
                    Description = "Produto de teste gerado na migration - Aurora",
                    ColorName = "Preto",
                    GTIN = "012345678910111213",
                    Active = true,
                    SKU = "202401",
                    MetricUnitName = "G",
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
                    ProductId = Guid.NewGuid(),
                    Name = "Motorola Moto E",
                    Description = "Produto de teste gerado na migration - SampleCompany",
                    ColorName = "Azul-Marinho",
                    GTIN = "012345678910111213",
                    MetricUnitName = "G",
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
        private static void SeedAgentTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentType>().HasData(
                
                new AgentType { AgentTypeId = 2, AgentTypeName = "Customer" },
                new AgentType { AgentTypeId = 3, AgentTypeName = "Employee" },
                new AgentType { AgentTypeId = 4, AgentTypeName = "Vendor" }
                
            );
        }

        private static void SeedAgent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>().HasData(
                new Agent
                {
                    AgentId = Guid.Parse("ca7f59ef-02aa-45f0-af27-91da78da253f"),
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    Name = "Olheiras Clinica Oftalmologica", 
                    AgentTypeId = 2,
                    Active = true,
                },
                new Agent
                {
                    AgentId = Guid.Parse("4c223cf3-a4ee-4bc3-82a0-763a73673114"),
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    Name = "Fastcar AutoParts",
                    AgentTypeId = 2,
                    Active = true,
                },
                new Agent
                {
                    AgentId = Guid.Parse("a5c4423a-4e92-4f3d-a4eb-89f1cd1a03d7"),
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    Name = "Speed Turbo Mecanica e Performance",
                    AgentTypeId = 2,
                    Active = true,
                }
            );
        }

        private static void SeedPhone(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().HasData(
                new Phone
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    PhoneId = Guid.Parse("7c6a7504-6747-4a5d-b2df-c43484371138"),
                    AgentId = Guid.Parse("ca7f59ef-02aa-45f0-af27-91da78da253f"),
                    AreaCode = "55",
                    PhoneNumber = "11955506737",
                    Primary = true,

                }
            );
        }

        private static void SeedEmail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>().HasData( 
                new Email
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    EmailAddressId = Guid.Parse("5f26a4d5-e05f-4d07-ba3d-25324b567b00"),
                    AgentId = Guid.Parse("ca7f59ef-02aa-45f0-af27-91da78da253f"),
                    EmailAddress = "lbruni10@gmail.com",
                    Primary = true,
                },
                new Email
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    EmailAddressId = Guid.Parse("709d8de8-bdb0-4703-81d0-e8d3764ed263"),
                    AgentId = Guid.Parse("ca7f59ef-02aa-45f0-af27-91da78da253f"),
                    EmailAddress = "leo.bruni130@gmail.com",
                    Primary = false,
                }
            );
        }

        private static void SeedAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    AgentId = Guid.Parse("ca7f59ef-02aa-45f0-af27-91da78da253f"),
                    AddressId = Guid.Parse("6ac52fbb-db9b-4210-9160-aac4a39285c3"),
                    AddressTypeId = 1,
                    CountryId = 1,
                    CountryName = "Brazil",
                    StateId = 1,
                    StateName = "Sao Paulo",
                    CityId =  1,
                    CityName = "Atibaia",
                    Reference = "1350",
                    PostalCode = "12947320",
                    StreetName = "Avenida Paulista",
                    StreetNumber = "1337",
                    Primary = true,
                    Active = true,
                }
            );
        }

        private static void SeedProfile(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().HasData(
                new Profile 
                {
                    TenantId = Guid.Parse("cabaa57a-37ff-4871-be7d-0187ed3534a5"),
                    ProfileId = Guid.Parse("bf489dd5-c2d6-444d-a761-4184b6471b96"),
                    AgentId = Guid.Parse("4c223cf3-a4ee-4bc3-82a0-763a73673114"),
                    CNAE = null,
                    CNPJ = "1234556789",
                    CPF = "9876544321",
                    DisplayName = "Leonardo B.",
                    FirstName = "Leonardo",
                    LastName = "Bruni",
                    Active = true
                }
            );
        }
    }
}
