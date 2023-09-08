using Backend.Domain.Entities.Authentication.Users.UserContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Reflection;

namespace Backend.API.Helpers.Controllers.Extensions
{
    public class CustomController : ControllerBase
    {
        protected class UserRoute
        {
            public Guid? RouteId { get; set; }
            public int? RouteCode { get; set; }
            public string RouteName { get; set; }
            public bool Access { get; set; }
        }
        public class ForbiddenAccessException : Exception
        {
            public ForbiddenAccessException(string message) : base(message) { }
        }
        protected Context LoadUserContext()
        {
            try
            {
                var SessionContext = HttpContext.Session;
                UserContextResponse userContext = Session.Extensions.SessionExtensions.Get<UserContextResponse>(SessionContext, "UserContext");
                List<UserRoute> userRoutes = VerifyUserRequest(userContext);
                Context context = new Context();
                if (userRoutes.Count() > 0)
                {
                    return context = new Context()
                    {
                        Success = false,
                        Message = "User does not have permission to use this resource."
                    };
                }
                else
                {
                    return context = new Context()
                    {
                        Claims = userContext.Claims,
                        Token = userContext.Token,
                        Success = true,
                        Message = "Access granted."
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected IActionResult HandleForbiddenAccessException(Exception ex)
        {
            if (ex is ForbiddenAccessException)
            {
                return StatusCode(403, ex.Message);
            }
            else
            {
                // Handle other exceptions as needed
                return StatusCode(500, "An error occurred.");
            }
        }
        protected List<UserRoute> VerifyUserRequest(UserContextResponse userContext)
        {
            try
            {
                // I should also implement an int Id and use Enum here besides only the string match
                var requestInfo = this.Url.ActionContext.ActionDescriptor.AttributeRouteInfo.Template.ToString();
                string routeName = requestInfo.Substring(0, requestInfo.IndexOf('/'));
                List<UserRoute> userRoutes = new List<UserRoute>();
                foreach (var module in userContext.Claims.Select(x => x.Modules).ToList())
                {
                    if (module.Any(x => x.Name == routeName))
                    {
                        userRoutes.Add(new UserRoute()
                        {
                            RouteId = module.First().Id,
                            RouteCode = null,
                            RouteName = module.First().Name,
                            Access = true
                        });
                    }
                }
                return userRoutes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
