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
        public Guid TenantId { get; set; }
        public Guid RoleId { get; set; }
        public int SubscriptionId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<Module> Modules { get; set; }
    }
}
