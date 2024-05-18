using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

namespace Frontend.Web.Repository.TenantRepository
{
    public class TenantRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public TenantRepository(HttpClientRepository httpClientRepository) 
        { 
            _httpClientRepository = httpClientRepository;
        }
        public async Task<Tenant> GetTenantById(Guid tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Default.FIND, ParameterValue = tenantId.ToString() };
            var request = new RouteBuilder<Tenant>().Send(Endpoints.Tenant, Methods.Default.FIND, parameters);
            return (await _httpClientRepository.Find(request)).Result;
        }
        public async Task<IEnumerable<Tenant>> GetTenantsByUserId(Guid userId) 
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Tenant.GetTenantsByUserIdParameters.userId, ParameterValue = userId.ToString() };
            var request = new RouteBuilder<Tenant>().Send(Endpoints.Membership, Methods.Tenant.GetTenantsByUserId, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }
        public async Task<ApiResponse<Tenant>> SetTenant(Guid tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Authentication.SetTenantParameters.tenantId, ParameterValue = tenantId.ToString() };
            var request = new RouteBuilder<Tenant>().Send(Endpoints.Authentication, Methods.Authentication.SetTenant, parameters);
            return await _httpClientRepository.Post<Tenant, Tenant>(request);
        }
    }
}
