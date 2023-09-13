using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Reflection;

namespace Backend.API.Helpers.Controllers.Extensions
{
    public class CustomController : ControllerBase
    {
        private readonly UserContextService _userContextService;
        public CustomController(UserContextService userContextService)
        {
            _userContextService = userContextService;
        }
        protected UserSessionContext LoadUserContext()
        {
            try
            {
                var SessionContext = HttpContext.Session;
                UserContextResponse userContext = Session.Extensions.SessionExtensions.Get<UserContextResponse>(SessionContext, "UserContext");
                var requestInfo = this.Url.ActionContext.ActionDescriptor.AttributeRouteInfo.Template.ToString();
                return _userContextService.Handler(userContext, requestInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("GetUserContext")]
        public UserSessionContext GenerateAndValidateContext()
        {
            var userSessionContext = LoadUserContext();
            return userSessionContext;
        }
    }
}
