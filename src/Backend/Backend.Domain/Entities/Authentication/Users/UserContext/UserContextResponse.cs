using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authorization.UserRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users.UserContext
{
    public class UserSessionContext 
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Tenant Tenant { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<UserRoute> Levels { get; set; }

    }
}
