using Backend.API.Util.Session.Extensions;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using SessionExtensions = Backend.API.Util.Session.Extensions.SessionExtensions;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateUserContextAttribute : ActionFilterAttribute
{
    private readonly IMemoryCache _cache;
    public ValidateUserContextAttribute(IMemoryCache cache)
    {
        _cache = cache;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string tokenRequest = context.HttpContext.Request.Headers.Authorization.ToString();
     
        /* 
         * BUG - 4/10/23
         * After implementing the context session storage in the web app for some unknown reason
         * when saving the session in the API app runs, it's not saving it.
         * need to figure out WHY the context API is not being saved when login is triggered by the UI.
         */
        var userContext = _cache.Get<UserSessionContext>(tokenRequest);

        // If there is no userContext it probably mean the user is not fucking logged in
        if (userContext == null)
        {
            context.Result = new UnauthorizedObjectResult(userContext)
            {
                StatusCode = 401,
                Value = "User is not autheticated"
            };
            return; // Fuck off 
        }

        if (tokenRequest == userContext.Token)
        {
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
        else
        {
            context.Result = new UnauthorizedObjectResult(userContext)
            {
                StatusCode = 401,
                Value = "User authorization is not valid"
            };
            return; // Fuck off 
        }
    }
}
