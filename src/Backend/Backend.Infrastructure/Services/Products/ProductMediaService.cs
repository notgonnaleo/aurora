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
    public class ProductMediaService : Service
    {
        private readonly AppDbContext _appDbContext;

        public ProductMediaService(AppDbContext appDbContext, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ProductMedia> Get(Guid tenantId, Guid productId)
        {
            return _appDbContext.ProductMedia
                .Where(x => x.TenantId == tenantId && x.ProductId == productId && x.Active)
                .ToList();
        }

        public ProductMedia? GetById(Guid tenantId, Guid productId, Guid productMediaId)
        {
            return _appDbContext.ProductMedia
                .Where(x => x.TenantId == tenantId && x.ProductId == productId && x.Id == productMediaId && x.Active)
                .FirstOrDefault();
        }

        public async Task<ProductMedia> Add(ProductMedia productMedia)
        {
            var context = LoadContext();
            productMedia.TenantId = context.Tenant.Id;

            _appDbContext.ProductMedia.Add(productMedia);
            if (await _appDbContext.SaveChangesAsync() > 0)
                return productMedia;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool Update(ProductMedia productMedia)
        {
            var context = LoadContext();
            productMedia.TenantId = context.Tenant.Id;
            productMedia.Updated = DateTime.UtcNow;
            productMedia.UpdatedBy = context.UserId;
            _appDbContext.Update(productMedia);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid tenantId, Guid productId, Guid Id)
        {
            var context = LoadContext();
            ProductMedia product = _appDbContext.ProductMedia
                .Where(x => x.Id == Id && x.TenantId == tenantId && x.ProductId == productId)
                .First();

            product.Active = false;
            _appDbContext.Update(product);
            return _appDbContext.SaveChanges() > 0;
        }
        
    }
}
