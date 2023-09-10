using Backend.API.Helpers.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateUserContextAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller as CustomController;
        var validationResult = controller.GenerateAndValidateContext();
        if (!validationResult.Success)
            context.Result = new UnauthorizedObjectResult(validationResult)
            {
                StatusCode = 401,
                Value = validationResult.Message
            };
    }
}
