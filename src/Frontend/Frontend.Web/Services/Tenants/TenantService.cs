using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Frontend.Web.Repository.TenantRepository;
using Frontend.Web.Services.Authentication;

namespace Frontend.Web.Services.Tenants
{
    public class TenantService
    {
        private readonly TenantRepository _tenantRepository;
        private readonly AuthenticationService _authenticationService;
        public TenantService(TenantRepository tenantRepository, AuthenticationService authenticationService) 
        { 
            _tenantRepository = tenantRepository;
            _authenticationService = authenticationService;
        }
        public async Task<Tenant> GetTenantById(Guid tenantId)
        {
            return await _tenantRepository.GetTenantById(tenantId);
        }
        public async Task<IEnumerable<Tenant>> GetTenantsByUserId()
        {
            UserSessionContext context = await _authenticationService.GetContext();
            return await _tenantRepository.GetTenantsByUserId(context.UserId);
        }
        public async Task<Tenant> SetTenant(Guid tenantId)
        {
            return await _tenantRepository.SetTenant(tenantId);
        }
    }
}
