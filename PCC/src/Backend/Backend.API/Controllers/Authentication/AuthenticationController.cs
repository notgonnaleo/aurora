using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend.Infrastructure.Services.Authorization.AuthorizationService;

namespace Backend.API.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly AuthorizationService _authorizationService;
        public AuthenticationController(AuthenticationService authenticationService, AuthorizationService authorizationService) 
        { 
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        public class UserContextResponse
        {
            public List<UserPermissions> perms { get; set; }
            public string Token { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var response = _authenticationService.Authenticate(request);
            if (response.Success)
            {
                List<UserPermissions> userPermissions = await _authorizationService.GetUserContext(response.Tenants, response.UserId);

                UserContextResponse userContextResponse = new UserContextResponse()
                {
                    perms = userPermissions,
                    Token = response.Token
                };
                return Ok(userContextResponse);
            }
            else
            {
                return NotFound(response.Message);
            }
        }
    }
}
