using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticationController(AuthenticationService authenticationService) 
        { 
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult Login(LoginRequest request)
        {
            var response = _authenticationService.Authenticate(request);
            return response.Success ? Ok(response.Token) : NotFound(response.Message);
        }
    }
}
