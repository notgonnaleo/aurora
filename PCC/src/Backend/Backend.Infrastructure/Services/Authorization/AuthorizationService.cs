using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authorization.Modules;
using Backend.Domain.Entities.Authorization.Roles;
using Backend.Domain.Entities.Authorization.UserRoles;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public class UserPermissions
        {
            public Guid TenantId { get; set; }
            public Guid UserId { get; set; }
            public Role Role { get; set; }
            public List<Module> Modules { get; set; }
        }

        public async Task<UserPermissions> GetUserRoles(List<Tenant> tenants, Guid userId)
        {
            /*
             * One User can be linked to various tenants (1:N)
             * So after the user realize his login, he can choose which tenant he wants to manage.
             */

            // need to figure out how can i return a list with:
            /*
            - user id
            - list of tenants (ids)
            - role (name and id)
            - list of modules (name and ids)
             */

            UserRole userRole = _authDbContext.UserRoles
                .Where(x => x.UserId == userId && x.TenantId == tenantId)
                .Include(i => i.RoleId)
                .First();

            Role role = _authDbContext.Roles
                .Where(x => x.TenantId == tenantId && x.Id == userRole.RoleId)
                .First();

            List<Module> modules = _authDbContext.Modules
                .Where(x => x.Id == role.ModuleId)
                .ToList();

            UserPermissions userPermissions = new UserPermissions
            {
                TenantId = tenantId,
                UserId = userId,
                Role = role,
                Modules = modules
            };

            return userPermissions;
        }
    }
}
