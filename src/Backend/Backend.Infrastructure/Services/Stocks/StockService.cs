using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.Stocks;
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
using Stock = Backend.Domain.Entities.Stocks.Stock;
using MovementStatus = Backend.Domain.Enums.StockMovements.MovementStatus;
using MovementTypes = Backend.Domain.Enums.StockMovements.MovementType.MovementTypes;
using Backend.Domain.Enums.StockMovements.MovementType;
using Backend.Domain.Enums.EnumExtensions;

namespace Backend.Infrastructure.Services.Stocks
{
    public class StockService : Service
    {

        private readonly AppDbContext _appDbContext;
        private readonly AgentService _agentService;
        private readonly ProductService _productService;
        private readonly ProductVariantService _productVariantService;

        public StockService(AppDbContext appDbContext, AgentService agentService, UserContextService main, ProductService productService, ProductVariantService productVariantService)
            : base(main)
        {
            _appDbContext = appDbContext;
            _agentService = agentService;
            _productService = productService;
            _productVariantService = productVariantService;

        }

        public Stock Add(Stock stock)
        {
            stock.StockMovementId = Guid.NewGuid();
            var context = LoadContext();
            ValidateTenant(stock.TenantId);

            // Yes we are just ovewritting some props, don't worry it's not magic, the other props still exists
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

        public IEnumerable<Stock> Get(Guid tenantId)
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

            ProductVariant variant = new ProductVariant();
            if (variantId is not null)
            {
                variant = _appDbContext.ProductVariants
                    .FirstOrDefault(x => x.VariantId == variantId);
            }

            var stockLogs = _appDbContext.Stocks
                .Where(x => x.TenantId == context.Tenant.Id &&
                x.ProductId == productId &&
                x.Active);

            var product = _productService.GetProductThumbnail(tenantId, productId);

            var totalQuantity = CalculateStock(stockLogs);

            return new Inventory()
            {
                Product = product,
                Variant = variant,
                TotalAmount = totalQuantity,
                Status = GenerateInventoryStatus(totalQuantity),
            };
        }

        public Domain.Entities.Stocks.MovementStatus GenerateInventoryStatus(int amount)
        {
            if (amount <= 0)
            {
                return new Domain.Entities.Stocks.MovementStatus()
                {
                    StatusId = (int)MovementStatus.OutOfStock,
                    StatusDescription = MovementStatus.OutOfStock.ToString()
                };
            }
            return new Domain.Entities.Stocks.MovementStatus()
            {
                StatusId = (int)MovementStatus.Available,
                StatusDescription = MovementStatus.Available.ToString()
            };
        }

        public IEnumerable<Inventory> GetInventory(Guid tenantId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            List<Inventory> inventory = new List<Inventory>();

            var allProducts = _productService.GetProductsWithDetail(context.Tenant.Id);

            foreach (var product in allProducts)
            {
                // Ideally this should be a own method but i dont care about it too much right now
                var variant = _appDbContext.ProductVariants
                    .FirstOrDefault(x => x.TenantId == tenantId &&
                    x.ProductId == product.ProductId &&
                    x.Active);

                var stockLogs = _appDbContext.Stocks
                    .Where(x => x.TenantId == context.Tenant.Id &&
                    x.ProductId == product.ProductId &&
                    x.Active);

                var totalQuantity = CalculateStock(stockLogs);
                inventory.Add(new Inventory()
                {
                    Product = product,
                    Variant = variant,
                    TotalAmount = totalQuantity,
                    Status = GenerateInventoryStatus(totalQuantity),
                });
            }
            return inventory;
        }

        public int CalculateStock(IEnumerable<Stock> stockLogs)
        {
            var amount = 0;
            foreach (var log in stockLogs)
            {
                if (log.MovementType == (int)MovementTypes.Input)
                {
                    amount += log.Quantity;
                }
                if (log.MovementType == (int)MovementTypes.Output)
                {
                    amount -= log.Quantity;
                }
                if (amount < 0) amount = 0;
            }
            return amount;
        }

        public IEnumerable<StockDetail> GetStockWithDetail(Guid tenantId)
        {
            IEnumerable<Stock> stock = _appDbContext.Stocks.Where(x => x.TenantId == tenantId && x.Active);
            List<ProductDetail> products = _productService.GetProductsWithDetail(tenantId).ToList();

            if (!products.Any() || products is null ) { 
                return Enumerable.Empty<StockDetail>();
            }

            return stock.Select(x => new StockDetail
            {
                TenantId = x.TenantId,
                VariantId = x.VariantId,
                StockMovementId = x.StockMovementId,
                ProductId = x.ProductId,
                MovementType = new Domain.Entities.Stocks.MovementTypes()
                {
                    MovementTypeId = x.MovementType.Value,
                    MovementTypeName = ((MovementTypes)x.MovementType.Value).GetDescription()
                },
                Active = x.Active,
                Quantity = x.Quantity,
                ProductName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.Name,
                ProductValue = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.Value ?? 0,
                SKU = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.SKU,
                GTIN = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.GTIN,
                CategoryName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.CategoryName,
                SubCategoryName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.SubCategoryName
            });
        }

        public IEnumerable<StockDetail> GetStockEntriesByProduct(Guid tenantId, Guid productId)
        {
            IEnumerable<Stock> stock = _appDbContext.Stocks
                .Where(x => x.TenantId == tenantId && x.ProductId == productId);
            ProductDetail product = _productService.GetProductThumbnail(tenantId, productId);

            return stock.Select(x => new StockDetail
            {
                TenantId = x.TenantId,
                VariantId = x.VariantId,
                StockMovementId = x.StockMovementId,
                ProductId = x.ProductId,
                MovementType = new Domain.Entities.Stocks.MovementTypes()
                {
                    MovementTypeId = x.MovementType.Value,
                    MovementTypeName = ((MovementTypes)x.MovementType.Value).GetDescription()
                },
                Active = x.Active,
                Quantity = x.Quantity,
                ProductName = product?.Name,
                ProductValue = product?.Value ?? 0,
                SKU = product?.SKU,
                GTIN = product?.GTIN,
                CategoryName = product?.CategoryName,
                SubCategoryName = product?.SubCategoryName
            });
        }

        public Stock? GetById(Guid tenantId, Guid stockMovementId)
        {
            var context = LoadContext();
            ValidateTenant(tenantId);
            return _appDbContext.Stocks
                .FirstOrDefault(x => x.StockMovementId == stockMovementId && 
                x.TenantId == context.Tenant.Id);
        }

        public bool Update(Stock model)
        {
            var context = LoadContext();
            ValidateTenant(model.TenantId);
            model.Updated = DateTime.Now;
            model.UpdatedBy = context.UserId;
            model.Active = true;
            _appDbContext.Update(model);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid tenantId,Guid stockMovementId)
        {
            var context = LoadContext();
            Stock stock = _appDbContext.Stocks.Where(x => x.StockMovementId == stockMovementId && x.TenantId == context.Tenant.Id).First();
            stock.Active = false;

            _appDbContext.Update(stock);
            var response = _appDbContext.SaveChanges();

            if (response <= 0)
                throw new Exception(Localization.GenericValidations.ErrorDeleteItem(context.Language));
            return true;
        }
    }

}
