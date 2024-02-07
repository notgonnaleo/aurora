using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Authorization.Modules;
using Backend.Domain.Entities.Authorization.Roles;
using Backend.Domain.Entities.Authorization.Subscriptions;
using Backend.Domain.Entities.Authorization.UserRoles;
using Backend.Domain.Entities.Authorization.UserRoutes;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Memberships;
using Backend.Infrastructure.Services.Tenants;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly UserContextService _userContextService;
        private readonly IMemoryCache _cache;
        private readonly TenantService _tenantService;

        public AuthorizationService(AuthDbContext authDbContext, UserContextService userContextService, IMemoryCache cache, TenantService tenantService) 
        { 
            _authDbContext = authDbContext;
            _userContextService = userContextService;
            _cache = cache;
            _tenantService = tenantService;
        }

        public IEnumerable<Claim> GetUserContext(Guid userId)
        {
            IEnumerable<Guid> tenantIds = _authDbContext.Memberships
                .Where(x => x.UserId == userId)
                .Select(x => x.TenantId);

            List<Claim> userPermissions = new List<Claim>();
            foreach (var tenantId in tenantIds.ToList())
            {
                UserRole userRole = _authDbContext.UserRoles
                    .Where(x => x.UserId == userId && x.TenantId == tenantId && x.Active == true)
                    .Include(i => i.RoleId)
                    .First();

                Role role = _authDbContext.Roles
                    .Where(x => x.TenantId == tenantId && x.Id == userRole.RoleId && x.Active == true)
                    .First();

                string roleName = _authDbContext.Subscriptions
                    .Where(x => x.Id == role.SubscriptionId && x.Active == true)
                    .Select(x => x.Name)
                    .First();

                List<Module> modules = _authDbContext.Modules
                    .Where(x => x.Id == role.ModuleId && x.Active == true)
                    .ToList();

                Claim claim = new Claim
                {
                    TenantId = tenantId,
                    RoleId = role.Id,
                    SubscriptionId = role.SubscriptionId,
                    RoleName = roleName,
                    Modules = modules
                };
                userPermissions.Add(claim);
            }
            return userPermissions;
        }
        public UserSessionContext MapUserContextRolesAndToken(LoginResponse response, IEnumerable<Claim> claims)
        {
            return new UserSessionContext()
            {
                UserId = response.UserId,
                Username = response.Username,
                Claims = claims,
                Token = response.Token,
                Language = Enums.Localization.LanguagesEnum.English, // TODO: Make this be changed by the user
                Levels = _userContextService.VerifyUserRequest(claims),
                Success = true,
                Tenant = _tenantService.GetById(claims.FirstOrDefault().TenantId),
                
            };
        }
        public Tenant SetTenant(Guid tenantId)
        {
            var context = _userContextService.LoadContext();
            Tenant newTenant = _tenantService.GetById(tenantId);
            UserSessionContext userSessionContext = new UserSessionContext()
            {
                Tenant = newTenant,
                UserId = context.UserId,
                Claims = context.Claims,
                Token = context.Token,
                Language = context.Language,
                Success = context.Success,
                Levels = context.Levels,
                Message = context.Message,
                Username = context.Username,
            };
            _cache.Set<UserSessionContext>(context.Token, userSessionContext);
            return newTenant;
        }
    }
}
