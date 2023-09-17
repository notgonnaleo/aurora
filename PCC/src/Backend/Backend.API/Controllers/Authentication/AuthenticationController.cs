using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Backend.Infrastructure.Services.Authorization.AuthorizationService;

namespace Backend.API.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly AuthorizationService _authorizationService;
        private readonly UserContextService _userContextService;
        public AuthenticationController(AuthenticationService authenticationService, AuthorizationService authorizationService, UserContextService userContextService) 
        { 
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
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
                Util.Session.Extensions.SessionExtensions.Set(HttpContext.Session, "UserContext", userContext);
                return Ok(userContext);
            }
            else
            {
                return NotFound(response.Message);
            }
        }
    }
}
