using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.Stocks;
using Backend.Domain.Enums.MovementType;
using Backend.Domain.Enums.StockMovements;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Enums.Localization;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using static Backend.Infrastructure.Enums.Modules.Methods;

namespace Backend.Infrastructure.Services.Stocks
{
    public class StockService : Service
    {

        private readonly AppDbContext _appDbContext;
        private readonly AgentService _agentService;
        private readonly ProductService _productService;


        public StockService(AppDbContext appDbContext, AgentService agentService, UserContextService main, ProductService productService)
            : base(main)
        {
            _appDbContext = appDbContext;
            _agentService = agentService;
            _productService = productService;
        }

        public Domain.Entities.Stocks.Stock Add(Domain.Entities.Stocks.Stock stock)
        {

            stock.StockMovementId = Guid.NewGuid();
            var context = LoadContext();
            ValidateTenant(stock.TenantId);

            stock.TenantId = context.Tenant.Id;
            stock.UserId = context.UserId;
            stock.MovementDate = DateTime.Now;
            stock.CreatedBy = context.UserId;
            stock.Created = DateTime.UtcNow;
            stock.Updated = null;
            stock.UpdatedBy = null;
            stock.Active = true;

            _appDbContext.Stocks.Add(stock);
            _appDbContext.SaveChanges();
            return stock;
        }

        public IEnumerable<Domain.Entities.Stocks.Stock> Get(Guid tenantId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            return _appDbContext.Stocks
                .Where(x => x.TenantId == context.Tenant.Id && x.Active == true)
                .ToList();
        }

        public Inventory GetProductStock(Guid tenantId, Guid productId, Guid? variantId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            Inventory inventory = new Inventory();
            var totalQuantity = 0;

            ProductVariant variant = new ProductVariant();
            if(variantId is not null)
            {
                variant = _appDbContext.ProductVariants
                    .FirstOrDefault(x => x.VariantId == variantId);
            }

            var stockLogs = _appDbContext.Stocks
                .Where(x => x.TenantId == context.Tenant.Id && 
                x.ProductId == productId && 
                x.Active);

            var product = _productService.GetProductThumbnail(tenantId, productId);

            foreach (var log in stockLogs)
            {
                if(log.MovementType == (int)MovementTypes.Input)
                {
                    totalQuantity += log.Quantity;
                }
                if(log.MovementType == (int)MovementTypes.Output)
                {
                    totalQuantity -= log.Quantity;
                }
                if(totalQuantity < 0) totalQuantity = 0;
            }

            return new Inventory()
            {
                Product = product,
                Variant = variant,
                TotalAmount = totalQuantity,
                Status = totalQuantity > 0 ? MovementStatus.Available : MovementStatus.OutOfStock,
            };
        }

        public IEnumerable<Inventory> GetInventory(Guid tenantId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            List<Inventory> inventory = new List<Inventory>();
            var totalQuantity = 0;

            var allProducts = _productService.GetProductsWithDetail(context.Tenant.Id);

            foreach (var product in allProducts)
            {
                var variant = _appDbContext.ProductVariants
                    .FirstOrDefault(x => x.ProductId == product.ProductId);

                var stockLogs = _appDbContext.Stocks
                    .Where(x => x.TenantId == context.Tenant.Id &&
                    x.ProductId == product.ProductId &&
                    x.Active);

                foreach (var log in stockLogs)
                {
                    if (log.MovementType == (int)MovementTypes.Input)
                    {
                        totalQuantity += log.Quantity;
                    }
                    if (log.MovementType == (int)MovementTypes.Output)
                    {
                        totalQuantity -= log.Quantity;
                    }
                    if (totalQuantity < 0) totalQuantity = 0;
                }

                inventory.Add(new Inventory()
                {
                    Product = product,
                    Variant = variant,
                    TotalAmount = totalQuantity,
                    Status = totalQuantity > 0 ? MovementStatus.Available : MovementStatus.OutOfStock,
                });
            }
            return inventory;
        }

        public IEnumerable<StockDetail> GetStockWithDetail(Guid tenantId)
        {
            IEnumerable<Domain.Entities.Stocks.Stock> stock = _appDbContext.Stocks.Where(x => x.TenantId == tenantId);
            List<ProductDetail> products = _productService.GetProductsWithDetail(tenantId).ToList();

            return stock.Select(x => new StockDetail
            {
                // Campos de Stock
                UserId = x.UserId,
                TenantId = x.TenantId,
                VariantId = x.VariantId,
                StockMovementId = x.StockMovementId,
                ProductId = x.ProductId,
                Active = x.Active,
                Quantity = x.Quantity,

                // Campos de Product
                ProductName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.Name,
                ProductValue = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.Value ?? 0,
                SKU = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.SKU,
                GTIN = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.GTIN,
                //VariantName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.VariantName,

                // Campos de Category e SubCategory
                CategoryName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.CategoryName,
                SubCategoryName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.SubCategoryName
            });
        }

        public Domain.Entities.Stocks.Stock? GetById(Guid tenantId, Guid stockMovementId)
        {
            var context = LoadContext();
            return _appDbContext.Stocks.FirstOrDefault(x => x.StockMovementId == stockMovementId && x.TenantId == context.Tenant.Id);
        }

        public bool Update(Domain.Entities.Stocks.Stock model)
        {
            var context = LoadContext();
            ValidateTenant(model.TenantId);
            model.Updated = DateTime.Now;
            model.UpdatedBy = context.UserId;
            model.Active = true;
            _appDbContext.Update(model);


            return _appDbContext.SaveChanges() > 0;


        }

        public bool Delete(Guid stockMovementId)
        {
            var context = LoadContext();
            Domain.Entities.Stocks.Stock stock = _appDbContext.Stocks.Where(x => x.StockMovementId == stockMovementId && x.TenantId == context.Tenant.Id).First();
            stock.Active = false;

            _appDbContext.Update(stock);
            var response = _appDbContext.SaveChanges();

            if (response <= 0)
                throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));

            return true;
        }




    }

}
