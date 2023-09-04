using Backend.Domain.Entities.Authentication.Users.Login.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users.Login.Response
{
    public class LoginResponse : LoginRequest
    {
        public string Token { get; set; }
    }
}
