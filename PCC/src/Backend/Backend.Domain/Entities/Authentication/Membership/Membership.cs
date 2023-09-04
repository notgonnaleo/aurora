using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Linkage
{
    // i fucking hate this shit

    [Table("Membership")]
    public class Membership
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
        [Required]
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
