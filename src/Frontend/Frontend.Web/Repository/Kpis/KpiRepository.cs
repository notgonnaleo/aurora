using Backend.Domain.Entities.Kpis;
using Backend.Domain.Entities.Orders;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

namespace Frontend.Web.Repository.Kpis
{
    public class KpiRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public KpiRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<ApiResponse<DashboardAnalytics>> GetDashboardAnalytics(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = "tenantId", ParameterValue = tenantId };
            var request = new RouteBuilder<DashboardAnalytics>().Send("Kpi", "GetDashboardAnalytics", parameters);
            return await _httpClientRepository.Find(request);
        }

        public async Task<ApiResponse<IEnumerable<Order>>> GetTransactionsAnalytics(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = "tenantId", ParameterValue = tenantId };
            var request = new RouteBuilder<Order>().Send("Kpi", "GetTransactionsAnalytics", parameters);
            return await _httpClientRepository.Get(request);
        }
    }
}
