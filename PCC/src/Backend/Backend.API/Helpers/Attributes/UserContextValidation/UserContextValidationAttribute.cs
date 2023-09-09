using Backend.API.Helpers.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateUserContextAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller as CustomController;

        // Since we don't have too much freedom here, the only thing this class
        // should do is just return the failure in case auth is not valid.
        if (!controller.GenerateAndValidateContext())
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
