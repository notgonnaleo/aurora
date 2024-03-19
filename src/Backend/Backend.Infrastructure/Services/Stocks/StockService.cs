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

        public IEnumerable<Domain.Entities.Stocks.Stock> Get()
        {
            var context = LoadContext();
            return _appDbContext.Stocks
                .Where(x => x.TenantId == context.Tenant.Id && x.Active == true)
                .ToList();
        }

        public IEnumerable<Stock> GetStockInventory()
        {

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
