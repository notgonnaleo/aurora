using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.ProductTypes;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(AppDbContext appDbContext, ProductTypeService productTypeService, IHttpContextAccessor httpContextAccessor, UserContextService main)
            : base(main)
        {
            _appDbContext = appDbContext;
            _productType = productTypeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Product> Get(Guid tenantId)
        {
            try
            {
                return _appDbContext.Products
                    .Where(x => x.TenantId == tenantId && x.Active)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Product? GetById(Guid tenantId, Guid productId)
        {
            try
            {
                return _appDbContext.Products
                    .Where(x => x.TenantId == tenantId && x.Id == productId && x.Active)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Product Add(Product product)
        {
            try
            {
                var context = LoadContext();
                product.TenantId = context.Tenant.Id;
                product = product.Create(product, context.UserId);
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                var context = LoadContext();
                product.TenantId = context.Tenant.Id;
                product = product.Update(product, context.UserId);
                _appDbContext.Update(product);
                return _appDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete(Guid Id)
        {
            try
            {
                var context = LoadContext();
                Product product = _appDbContext.Products.Where(x => x.Id == Id).First();
                product.Active = false;
                _appDbContext.Update(product);
                return _appDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<ProductDetail> GetProductsWithDetail(Guid tenantId)
        {
            try
            {
                var products = Get(tenantId);
                var types = _productType.Get();
                return products.Select(product => new ProductDetail
                {
                    TenantId = product.TenantId,
                    Id = product.Id,
                    ProductTypeId = product.ProductTypeId,
                    SKU = product.SKU,
                    Name = product.Name,
                    Description = product.Description,
                    Value = product.Value,
                    TotalWeight = product.TotalWeight,
                    LiquidWeight = product.LiquidWeight,
                    ProductType = types.First(x => x.Id == product.ProductTypeId),
                    ProductTypeName = types.First(x => x.Id == product.ProductTypeId).Name,
                    Created = product.Created,
                    CreatedBy = product.CreatedBy,
                    Updated = product.Updated,
                    UpdatedBy = product.UpdatedBy, 
                    Active = product.Active,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
