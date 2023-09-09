using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Authorization.UserRoles;
using Backend.Domain.Entities.Authorization.UserRoutes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Authorization
{
    public class UserContextService
    {
        public UserSessionContext Handler(UserContextResponse userContext, string requestInfo)
        {
            List<UserRoute> userRoutes = VerifyUserRequest(userContext, requestInfo);
            UserSessionContext context = new UserSessionContext();
            if (userRoutes.Count() < 0)
            {
                return context = new UserSessionContext()
                {
                    Success = false,
                    Message = "User does not have permission to use this resource."
                };
            }
            else
            {
                return context = new UserSessionContext()
                {
                    Claims = userContext.Claims,
                    Token = userContext.Token,
                    Success = true,
                    Message = "Access granted."
                };
            }
        }
        protected List<UserRoute> VerifyUserRequest(UserContextResponse userContext, string requestInfo)
        {
            try
            {
                // I should also implement an int Id and use Enum here besides only the string match
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
