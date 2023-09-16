using Backend.Domain.Entities.Authentication.Users.Claims;
using Backend.Domain.Entities.Authorization.UserRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users.UserContext
{
    // Rework my context model because this looks awful to access.
    public class UserContextResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public List<Claim> Claims { get; set; }
        public string Token { get; set; }
    }

    public class UserSessionContext : UserContextResponse 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<UserRoute> Levels { get; set; }
    }
}
