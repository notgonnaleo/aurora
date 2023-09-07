using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authentication.Users.Claims;
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

        public async Task<List<Claim>> GetUserContext(List<Tenant> tenants, Guid userId)
        {
            List<Claim> userPermissions = new List<Claim>();
            foreach (var tenant in tenants)
            {
                UserRole userRole = _authDbContext.UserRoles
                    .Where(x => x.UserId == userId && x.TenantId == tenant.Id)
                    .Include(i => i.RoleId)
                    .First();

                Role role = _authDbContext.Roles
                    .Where(x => x.TenantId == tenant.Id && x.Id == userRole.RoleId)
                    .First();

                List<Module> modules = _authDbContext.Modules
                    .Where(x => x.Id == role.ModuleId)
                    .ToList();

                Claim claim = new Claim
                {
                    Tenant = tenant,
                    UserId = userId,
                    Role = role,
                    Modules = modules
                };
                userPermissions.Add(claim);
            }
            return userPermissions.ToList();
        }
    }
}
