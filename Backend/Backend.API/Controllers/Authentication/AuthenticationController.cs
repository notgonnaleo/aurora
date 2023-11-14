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
            // maybe create a tenant list explicit would be easier
            var response = _authenticationService.Authenticate(request);
            if (response.Success)
            {
                List<Claim> userPermissions = await _authorizationService.GetUserContext(response.Tenants, response.UserId);
                UserSessionContext userContext = new UserSessionContext()
                {
                    UserId = response.UserId,
                    Username = response.Username,
                    Claims = userPermissions,
                    Token = response.Token,
                    Levels = _userContextService.VerifyUserRequest(userPermissions),
                    Success = true
                };

                // It will store the userContext on the cache and it can be found by getting it using the token
                _cache.Set(userContext.Token, userContext, TimeSpan.FromHours(4));
                return Ok(userContext);
            }
            else
            {
                return NotFound(response.Message);
            }
        }
    }
}
