using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.UserRoles
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
        [JsonIgnore]
        public virtual Tenant? Tenant { get; set; }
    }
}
