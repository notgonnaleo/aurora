using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Login(LoginRequest request)
        {
            var response = _authenticationService.Authenticate(request);
            var userPermissions = _authorizationService.GetUserRoles(response.Tenants, response.UserId);
            return response.Success ? Ok(response.Token) : NotFound(response.Message);
        }
    }
}
