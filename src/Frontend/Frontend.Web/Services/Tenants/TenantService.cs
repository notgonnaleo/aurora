using Backend.Domain.Entities.Authentication.Tenants;
using Frontend.Web.Repository.TenantRepository;

namespace Frontend.Web.Services.Tenants
{
    public class TenantService
    {
        private readonly TenantRepository _tenantRepository;
        public TenantService(TenantRepository tenantRepository) 
        { 
            _tenantRepository = tenantRepository;
        }
        public async Task<IEnumerable<Tenant>> GetTenantsByUserId(Guid userId)
        {
            return await _tenantRepository.GetTenantsByUserId(userId);
        }
    }
}
