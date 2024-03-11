using Frontend.Web.Repository.Stocks;
using Backend.Domain.Entities.Stock;
using Frontend.Web.Repository.Agents;
using Backend.Domain.Entities.Products;
using Frontend.Web.Services.Products;

namespace Frontend.Web.Services.Stocks
{
    public class StockService
    {
        private readonly StockRepository _stockRepository;
        private readonly ProductRepository _productRepository;

        public StockService(StockRepository stockRepository, ProductRepository productRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
        }

        public async Task<Backend.Domain.Entities.Stock.Stock> CreateStock(Backend.Domain.Entities.Stock.Stock stock)
        {
            return await _stockRepository.CreateStock(stock);
        }

        public async Task<IEnumerable<StockDetail>> GetStockWithDetail(string tenantId)
        {            
            var stocks = await _stockRepository.GetStockWithDetail(tenantId);
            IEnumerable<ProductDetail> products = await _productRepository.GetProducts(tenantId);          
            return stocks.Select(x => new StockDetail
            {
                // CAMPOS DO OBJETO STOCK
                UserId = x.UserId,
                TenantId = x.TenantId,
                VariantId = x.VariantId,
                StockMovementId = x.StockMovementId,
                ProductId = x.ProductId,
                MovementStatusId = x.MovementStatusId,
                Active = x.Active,
                Quantity = x.Quantity,
                MovementType = x.MovementType,

                // CAMPOS DE PRODUTO
                ProductName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.Name,
                
            });
        }


        public async Task<IEnumerable<Backend.Domain.Entities.Stock.Stock>> GetStock(string tenantId)
        {
            return await _stockRepository.GetStock(tenantId);
        }

        public async Task<Backend.Domain.Entities.Stock.Stock> GetById(string tenantId, string stockMovementId)
        {
            return await _stockRepository.GetById(tenantId, stockMovementId);
        }

        public async Task<bool> UpdateStock(Backend.Domain.Entities.Stock.Stock stock)
        {
            return await _stockRepository.UpdateStock(stock);
        }


    }
}
