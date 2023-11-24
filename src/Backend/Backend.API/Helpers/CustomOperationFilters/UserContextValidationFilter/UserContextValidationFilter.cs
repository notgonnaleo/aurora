using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

public class UserContextValidationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var customAttributes = context.MethodInfo.GetCustomAttributes<ValidateUserContextAttribute>(true)
            .Union(context.MethodInfo.DeclaringType.GetCustomAttributes<ValidateUserContextAttribute>(true));
    }
}