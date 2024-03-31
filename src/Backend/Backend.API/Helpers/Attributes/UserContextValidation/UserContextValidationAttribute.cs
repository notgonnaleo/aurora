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
        string tokenRequest = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer", "").Trim();
        var userContext = _cache.Get<UserSessionContext>(tokenRequest);

        if (userContext == null)
        {
            context.Result = new UnauthorizedObjectResult(userContext)
            {
                StatusCode = 401,
                Value = "User is not autheticated"
            };
            return;
        }

        _cache.Set("Token", tokenRequest, TimeSpan.FromHours(4));
        _cache.Set(userContext.Token, userContext, TimeSpan.FromHours(4));

        if (tokenRequest == userContext.Token)
        {
            foreach (var access in userContext.Levels)
            {
                if (!access.Access)
                {
                    context.Result = new UnauthorizedObjectResult(userContext)
                    {
                        StatusCode = 401,
                        Value = userContext.Message
                    };
                }
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult(userContext)
            {
                StatusCode = 401,
                Value = "User authorization is not valid"
            };
            return;
        }
    }
}
