using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authentication.Tenants
{
    [Table("Tenant")]
    public class Tenant
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
