using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;

public class UserContextValidationFilter : IOperationFilter
{
    // Mapping the custom attribute so we can instance it at the Custom Controller
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var customAttributes = context.MethodInfo.GetCustomAttributes<ValidateUserContextAttribute>(true)
            .Union(context.MethodInfo.DeclaringType.GetCustomAttributes<ValidateUserContextAttribute>(true));
    }
}