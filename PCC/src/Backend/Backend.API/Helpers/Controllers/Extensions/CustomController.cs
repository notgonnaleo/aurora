using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Reflection;

namespace Backend.API.Helpers.Controllers.Extensions
{
    /*
     * When I wrote this, only me and God knew what this was supposed to do.
     * Now, only God knows...
     */
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

        // Every request should call this method before running the entire action.
        [HttpGet] 
        public bool GenerateAndValidateContext()
        {
            var userSessionContext = LoadUserContext();
            return userSessionContext.Success;
        }
    }
}
