using Backend.Domain.Entities.Authentication.Tenants;
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
        public List<Tenant> Tenants { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
