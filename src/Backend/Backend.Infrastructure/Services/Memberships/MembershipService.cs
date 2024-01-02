using Backend.Domain.Entities.Authentication.Membership;
using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Memberships
{
    public class MembershipService
    {
        private readonly AuthDbContext _authDbContext;
        public MembershipService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public IEnumerable<Membership> GetUserMemberships(Guid userId)
        {
            return _authDbContext.Memberships.Where(x => x.UserId == userId);
        }
        public IEnumerable<Tenant> GetTenantsByUserId(Guid userId)
        {
            return _authDbContext.Memberships.Where(x => x.UserId == userId).Select(x => x.Tenant);
        }
    }
}
