using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.ProductTypes;
using Backend.Infrastructure.Services.SubCategories;
using Backend.Infrastructure.Services.Tenants;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly ProductTypeService _productType;
        private readonly CategoryService _categoryService;
        private readonly SubCategoryService _subCategoryService;

        public ProductService(AppDbContext appDbContext, ProductTypeService productTypeService, UserContextService main, CategoryService categoryService, SubCategoryService subCategoryService)
            : base(main)
        {
            _appDbContext = appDbContext;
            _productType = productTypeService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        public IEnumerable<Product> Get(Guid tenantId)
        {
            return _appDbContext.Products
                .Where(x => x.TenantId == tenantId && x.Active)
                .ToList();
        }

        public Product? GetById(Guid tenantId, Guid productId)
        {
            return _appDbContext.Products
                .Where(x => x.TenantId == tenantId && x.Id == productId && x.Active)
                .FirstOrDefault();
        }

        public async Task<Product> Add(Product product)
        {
            var context = LoadContext();
            product.TenantId = context.Tenant.Id;
            product = product.Create(product, context.UserId);
            product.ValidateFields();
            _appDbContext.Products.Add(product);

            if (await _appDbContext.SaveChangesAsync() > 0)
                return product;

            throw new Exception("Failure while saving the new item");
        }

        public bool Update(Product product)
        {
            var context = LoadContext();
            product.TenantId = context.Tenant.Id;
            product.Updated = DateTime.Now;
            product.UpdatedBy = context.UserId;
            product.ValidateFields();
            _appDbContext.Update(product);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid tenantId, Guid Id)
        {
            var context = LoadContext();
            Product product = _appDbContext.Products
                .Where(x => x.Id == Id && x.TenantId == tenantId)
                .First();

            product.Active = false;
            _appDbContext.Update(product);
            return _appDbContext.SaveChanges() > 0;
        }

        public IEnumerable<ProductDetail> GetProductsWithDetail(Guid tenantId)
        {
            var products = Get(tenantId);
            var types = _productType.Get();
            var categories = _categoryService.Get(tenantId);
            var subCategories = _subCategoryService.Get(tenantId);
            return products.Select(product => new ProductDetail
            {
                TenantId = product.TenantId,
                Id = product.Id,
                ProductTypeId = product.ProductTypeId,
                SKU = product.SKU,
                GTIN = product.GTIN,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                TotalWeight = product.TotalWeight,
                LiquidWeight = product.LiquidWeight,
                ProductType = types.First(x => x.Id == product.ProductTypeId),
                ProductTypeName = types.First(x => x.Id == product.ProductTypeId).Name,
                CategoryId = product.CategoryId ?? null,
                SubCategoryId = product.SubCategoryId ?? null,
                CategoryName = (product.CategoryId != null) ? categories.FirstOrDefault(x => x.CategoryId == product.CategoryId).CategoryName ?? string.Empty : string.Empty,
                SubCategoryName = (product.CategoryId != null) ? subCategories.FirstOrDefault(x => x.SubCategoryId == product.SubCategoryId).SubCategoryName ?? string.Empty : string.Empty,
                Created = product.Created,
                CreatedBy = product.CreatedBy,
                Updated = product.Updated,
                UpdatedBy = product.UpdatedBy,
                Active = product.Active,
            });
        }
    }
}
