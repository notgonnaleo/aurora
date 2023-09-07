using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
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
        public AuthenticationController(AuthenticationService authenticationService, AuthorizationService authorizationService) 
        { 
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var response = _authenticationService.Authenticate(request);
            if (response.Success)
            {
                List<Claim> userPermissions = await _authorizationService.GetUserContext(response.Tenants, response.UserId);
                UserContextResponse userContextResponse = new UserContextResponse()
                {
                    Claims = userPermissions,
                    Token = response.Token
                };
                // instead of using string figure out how can i use my own object
                HttpContext.Session.SetString("UserContext", JsonConvert.SerializeObject(userContextResponse));
                return Ok(userContextResponse);
            }
            else
            {
                return NotFound(response.Message);
            }
        }
    }
}
