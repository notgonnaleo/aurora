using Frontend.Web.Repository.Stocks;
using Backend.Domain.Entities.Stock;
using Frontend.Web.Repository.Agents;

namespace Frontend.Web.Services.Stocks
{
    public class StockService
    {
        private readonly StockRepository _stockRepository;

        public StockService(StockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Backend.Domain.Entities.Stock.Stock> CreateStock(Backend.Domain.Entities.Stock.Stock stock)
        {
            return await _stockRepository.CreateStock(stock);
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
