using Backend.Domain.Entities.Authentication.Tenants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Users
{
    public class User
    {
        // TODO: Create new table linking tenant with user

        public Guid Id { get; set; }
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
    }
}
