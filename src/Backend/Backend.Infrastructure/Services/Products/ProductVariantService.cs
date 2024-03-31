using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Infrastructure.Enums.Modules.Methods;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductVariantService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly ProductService _productService;

        public ProductVariantService(UserContextService userContextService, ProductService productService, AppDbContext appDbContext) : base(userContextService)
        {
            _appDbContext = appDbContext;
            _productService = productService;
        }

        public IEnumerable<ProductVariant> GetAllVariantsByTenant(Guid tenantId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId).ToList();
        }

        public IEnumerable<ProductVariant> GetAllVariantsByProduct(Guid tenantId, Guid productId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId && 
                x.ProductId == productId &&
                x.Active);
        }
        public IEnumerable<ProductVariant> GetVariant(Guid tenantId, Guid productId, Guid variantId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId &&
                x.ProductId == productId &&
                x.VariantId == variantId &&
                x.Active);
        }

        public async Task<ProductVariant> CreateVariant(ProductVariant model)
        {
            var context = LoadContext();
            model.TenantId = context.Tenant.Id;
            model = new ProductVariant(model, context.UserId);
            model.ValidateFields(context.Language);

            _appDbContext.ProductVariants.Add(model);
            if (await _appDbContext.SaveChangesAsync() > 0)
                return model;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public async Task<ProductVariant> CreateVariantBasedParentProduct(ProductVariant model)
        {
            if (model.TotalWeight is null && model.LiquidWeight is null || model.OverwriteValue)
            {
                var parentProduct = _productService.GetById(model.TenantId, model.ProductId);
                if(parentProduct is not null)
                {
                    return await CreateVariant(new ProductVariant()
                    {
                        TenantId = model.TenantId,
                        VariantId = model.VariantId, 
                        ProductId = parentProduct.ProductId,
                        SKU = new SKU(parentProduct.Name, parentProduct.ProductTypeId, model.ColorName).SKUValue,
                        GTIN = model.GTIN,
                        Name = model.Name,
                        Description = model.Description,
                        ColorName = model.ColorName,
                        LiquidWeight = parentProduct.LiquidWeight,
                        TotalWeight = parentProduct.TotalWeight,
                        Value = model.OverwriteValue ? parentProduct.Value : model.Value,
                        OverwriteValue = model.OverwriteValue,
                    });
                }
            }
            return await CreateVariant(model);

        }
    }
}
