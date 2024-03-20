using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
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
                .Where(x => x.TenantId == tenantId 
                && x.ProductId == productId 
                && x.Active)
                .FirstOrDefault();
        }

        public async Task<Product> Add(Product product)
        {
            var context = LoadContext();
            product.TenantId = context.Tenant.Id;
            product = new Product(product, context.UserId);
            product.ValidateFields(context.Language);

            _appDbContext.Products.Add(product);
            if (await _appDbContext.SaveChangesAsync() > 0)
                return product;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool Update(Product product)
        {
            var context = LoadContext();
            product.TenantId = context.Tenant.Id;
            product.Updated = DateTime.UtcNow;
            product.UpdatedBy = context.UserId;
            product.ValidateFields(context.Language);
            _appDbContext.Update(product);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid tenantId, Guid Id)
        {
            var context = LoadContext();
            Product product = _appDbContext.Products
                .Where(x => x.ProductId == Id && x.TenantId == tenantId)
                .First();

            product.Active = false;
            _appDbContext.Update(product);
            return _appDbContext.SaveChanges() > 0;
        }

        public IEnumerable<ProductDetail> GetProductsWithDetail(Guid? tenantId)
        {
            // Im too lazy to be getting ids so whatever
            if(tenantId == Guid.Empty || tenantId is null)
            {
                var context = LoadContext();
                tenantId = context.Tenant.Id;
            }
            var products = Get(tenantId.Value);
            var types = _productType.Get();
            var categories = _categoryService.Get();
            var subCategories = _subCategoryService.Get();
            return products.Select(product => new ProductDetail
            {
                TenantId = product.TenantId,
                ProductId = product.ProductId,
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

        public ProductDetail GetProductThumbnail(Guid tenantId, Guid productId)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.TenantId == tenantId &&
            x.ProductId == productId &&
            x.Active);

            var types = _productType.Get();
            var category = _categoryService.GetCategoryAndSubCategoriesById(product.CategoryId.Value);

            return new ProductDetail
            {
                TenantId = product.TenantId,
                ProductId = product.ProductId,
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
                CategoryName = category.CategoryName,
                SubCategoryName = // need to finish this shit lmao,
                Created = product.Created,
                CreatedBy = product.CreatedBy,
                Updated = product.Updated,
                UpdatedBy = product.UpdatedBy,
                Active = product.Active,
            };

        }
    }
}
