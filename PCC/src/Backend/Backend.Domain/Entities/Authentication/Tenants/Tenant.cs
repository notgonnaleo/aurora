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
    // Yeah smartass we cant log 100% who the fuck created this tenant or updated it but im 
    // too lazy to fix it or even change it + i dont care + do better + kys + ratio

    // ayo i need to add a custom code on it because writing an entire guid code all the time is not too cool
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
