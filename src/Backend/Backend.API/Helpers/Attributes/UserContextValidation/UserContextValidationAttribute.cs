using Backend.API.Util.Session.Extensions;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SessionExtensions = Backend.API.Util.Session.Extensions.SessionExtensions;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateUserContextAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userContext = SessionExtensions.Get<UserSessionContext>(context.HttpContext.Session, "UserContext");

        // If there is no userContext it probably mean the user is not fucking logged in
        if(userContext == null)
        {
            context.Result = new UnauthorizedObjectResult(userContext)
            {
                StatusCode = 401,
                Value = "User is not autheticated!"
            };
            return; // Fuck off 
        }

        foreach (var access in userContext.Levels) // i have no idea on wtf im doing | << chill out you doing fine
        {
            if (!access.Access)
                context.Result = new UnauthorizedObjectResult(userContext)
                {
                    StatusCode = 401,
                    Value = userContext.Message
                };
        }
    }
}
