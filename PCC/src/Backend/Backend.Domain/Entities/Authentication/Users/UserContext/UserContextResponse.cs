using Backend.Domain.Entities.Authentication.Users.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users.UserContext
{
    public class UserContextResponse
    {
        public List<Claim> Claims { get; set; }
        public string Token { get; set; }
    }

    public class UserSessionContext : UserContextResponse 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
