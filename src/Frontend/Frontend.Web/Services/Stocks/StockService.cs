using Frontend.Web.Repository.Stocks;
using Backend.Domain.Entities.Stocks;
using Frontend.Web.Repository.Agents;
using Backend.Domain.Entities.Products;
using Frontend.Web.Services.Products;
using Frontend.Web.Repository.Contacts.Frontend.Web.Services.Profiles;

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

        public async Task<Backend.Domain.Entities.Stocks.Stock> CreateStock(Backend.Domain.Entities.Stocks.Stock stock)
        {
            return await _stockRepository.CreateStock(stock);
        }

        public async Task<IEnumerable<StockDetail>> GetStockWithDetail(string tenantId)
        {
            return await _stockRepository.GetStockWithDetail(tenantId);
        }

        public async Task<IEnumerable<Backend.Domain.Entities.Stocks.Stock>> GetStocks(string tenantId)
        {
            return await _stockRepository.GetStocks(tenantId);
        }

        public async Task<Backend.Domain.Entities.Stocks.Stock> GetStock(string tenantId, string stockMovementId)
        {
            return await _stockRepository.GetStock(tenantId, stockMovementId);
        }

        public async Task<bool> UpdateStock(Backend.Domain.Entities.Stocks.Stock stock)
        {
            return await _stockRepository.UpdateStock(stock);
        }

        public async Task<IEnumerable<Inventory>> GetInventory(string tenantId)
        {
            return await _stockRepository.GetInventory(tenantId);
        }

        public async Task<IEnumerable<StockDetail>> GetStockEntriesByProduct(string tenantId, string productId)
        {
            return await _stockRepository.GetStockEntriesByProduct(tenantId, productId);
        }
        public async Task<bool> DeleteStock(string tenantId, string stockMovementId)
        {
            return await _stockRepository.DeleteStock(tenantId, stockMovementId);
        }

    }

}