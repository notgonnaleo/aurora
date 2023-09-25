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
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
