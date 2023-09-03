using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Tenants
{
    public class TenantService
    {
        private readonly AuthDbContext _authDbContext;
        public TenantService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<IEnumerable<Tenant>> Get()
        {
            return _authDbContext.Tenants.Where(x => x.Active == true).ToList();
        }
    }
}
