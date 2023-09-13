using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authorization.Modules;
using Backend.Domain.Entities.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users.Claims
{
    public class Claim
    {
        public Tenant Tenant { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
        public List<Module> Modules { get; set; }
    }
}
