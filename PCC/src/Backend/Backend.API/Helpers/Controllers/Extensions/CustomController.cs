using Backend.Domain.Entities.Authentication.Users.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Helpers.Controllers.Extensions
{
    public class CustomController : ControllerBase
    {
        protected UserContextResponse LoadUserContext()
        {
            var SessionContext = HttpContext.Session;
            UserContextResponse userContext = Session.Extensions.SessionExtensions.Get<UserContextResponse>(SessionContext, "UserContext");
            return userContext;
        }
    }
}
