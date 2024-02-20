using Backend.API.Util.Session.Extensions;
using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Authorization.UserRoles;
using Backend.Domain.Entities.Authorization.UserRoutes;
using Backend.Infrastructure.Enums.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SessionExtensions = Backend.API.Util.Session.Extensions.SessionExtensions;

namespace Backend.Infrastructure.Services.Authorization
{
    public class UserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public UserContextService(IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }
        public UserSessionContext Handler(UserSessionContext userContext)
        {
            IEnumerable<UserRoute> userRoutes = VerifyUserRequest(userContext.Claims);
            if (userRoutes.Count() < 0)
            {
                return userContext = new UserSessionContext()
                {
                    Success = false,
                    Message = "User does not have permission to use this resource."
                };
            }
            else
            {
                return userContext = new UserSessionContext()
                {
                    UserId = userContext.UserId,
                    Username = userContext.Username,
                    Claims = userContext.Claims,
                    Token = userContext.Token,
                    Levels = userContext.Levels,
                    Tenant = userContext.Tenant,
                    Language = userContext.Language,
                    Success = true,
                    Message = "Access granted."
                };
            }
        }

        public UserSessionContext LoadContext()
        {
            var retrieveAuthToken = _cache.Get<string>("Token");
            if (retrieveAuthToken == null)
                throw new Exception("Invalid or missing authorization token.");
            var userContext = _cache.Get<UserSessionContext>(retrieveAuthToken);
            return Handler(userContext);
        }

        public IEnumerable<UserRoute> VerifyUserRequest(IEnumerable<Claim> userClaims)
        {
            try
            {
                List<UserRoute> userRoutes = new List<UserRoute>();
                foreach (var module in userClaims.SelectMany(x => x.Modules))
                    if (Enum.GetValues(typeof(ModulesEnum)).Cast<int>().Contains(module.Id))
                        userRoutes.Add(new UserRoute()
                        {
                            RouteId = module.Id,
                            RouteName = module.Name,
                            Access = true
                        });
                return userRoutes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
