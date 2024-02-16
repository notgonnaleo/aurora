using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Products
{
    public class ProductVariantService : Service
    {
        private readonly AppDbContext _appDbContext;

        public ProductVariantService(UserContextService userContextService, AppDbContext appDbContext) : base(userContextService)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ProductVariant> GetAllVariantsByTenant(Guid tenantId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId).ToList();
        }

        public IEnumerable<ProductVariant> GetAllVariantsByProduct(Guid tenantId, Guid productId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId && x.ProductId == productId).ToList();
        }
        public IEnumerable<ProductVariant> GetVariant(Guid tenantId, Guid productId, Guid variantId)
        {
            return _appDbContext.ProductVariants
                .Where(x => x.TenantId == tenantId && 
                x.ProductId == productId && 
                x.VariantId == variantId).ToList();
        }
    }
}
