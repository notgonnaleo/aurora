using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authorization.Roles;
using Backend.Domain.Entities.Authorization.UserRoles;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Authorization
{
    public class AuthorizationService
    {
        private readonly AuthDbContext _authDbContext;
        public AuthorizationService(AuthDbContext authDbContext) 
        { 
            _authDbContext = authDbContext;
        }

        //public async Task<IEnumerable<UserRole>> GetUserRoles(Guid TenantId, Guid UserId)
        //{
        //    List<UserRole> userRoles = _authDbContext.UserRoles.Where(x => x.UserId == UserId && x.TenantId == TenantId).ToList();
        //    List<Role> roles = _authDbContext.Roles.Where(x => x.TenantId == TenantId).ToList();
        //    User user = _authDbContext.Users.Where(x => x.Id == UserId).First();

        //    return;
        //}
    }
}
