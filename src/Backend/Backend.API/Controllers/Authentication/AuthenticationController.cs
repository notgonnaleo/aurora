using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Backend.Infrastructure.Services.Authorization.AuthorizationService;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Microsoft.Extensions.Caching.Memory;
using Backend.Domain.Entities.Authentication.Tenants;

namespace Backend.API.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly AuthorizationService _authorizationService;
        private readonly UserContextService _userContextService;
        private readonly IMemoryCache _cache;
        public AuthenticationController(AuthenticationService authenticationService, AuthorizationService authorizationService, UserContextService userContextService, IMemoryCache cache) 
        { 
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _cache = cache;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserSessionContext>> Login(LoginRequest request)
        {
            var response = _authenticationService.Authenticate(request);
            if (response.Success)
            {
                IEnumerable<Claim> claims = _authorizationService.GetUserContext(response.UserId);
                UserSessionContext userContext = _authorizationService.MapUserContextRolesAndToken(response, claims);
                _cache.Set(userContext.Token, userContext, TimeSpan.FromHours(4));
                return Ok(userContext);
            }
            return NotFound(response.Message);
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("SetTenant")]
        public async Task<ActionResult<Tenant>> SetTenant(Guid tenantId)
        {
            return _authorizationService.SetTenant(tenantId);
        }
    }
}
