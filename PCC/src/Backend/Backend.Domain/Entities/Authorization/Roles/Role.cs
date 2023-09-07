using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authorization.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.Roles
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        [ForeignKey("Module")]
        public Guid ModuleId { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
        [Required]
        [JsonIgnore]
        public virtual Module Module { get; set; }
    }
}
