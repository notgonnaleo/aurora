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

        public IEnumerable<ProductMedia> Get(Guid productId)
        {
            var context = LoadContext();
            return _appDbContext.ProductMedia
                .Where(x => x.TenantId == context.Tenant.Id && x.ProductId == productId && x.Active)
                .ToList();
        }

        public ProductMedia? GetById(Guid id)
        {
            var context = LoadContext();
            return _appDbContext.ProductMedia
                .FirstOrDefault(x => x.TenantId == context.Tenant.Id && x.Id == id && x.Active);
        }

        public async Task<ProductMedia> Add(ProductMedia model)
        {
            var context = LoadContext();
            model.TenantId = context.Tenant.Id;

            _appDbContext.ProductMedia.Add(model);
            if (await _appDbContext.SaveChangesAsync() > 0)
                return model;

            throw new Exception(Localization.GenericValidations.ErrorSaveItem(context.Language));
        }

        public bool Update(ProductMedia model)
        {
            var context = LoadContext();
            if(model.TenantId != context.Tenant.Id) throw new Exception(Localization.GenericValidations.ErrorWrongTenant(context.Language));

            model.Updated = DateTime.UtcNow;
            model.UpdatedBy = context.UserId;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var context = LoadContext();

            ProductMedia? model = _appDbContext.ProductMedia.FirstOrDefault(x => x.Id == id && x.TenantId == context.Tenant.Id) ?? throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            model.Active = false;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

    }
}
