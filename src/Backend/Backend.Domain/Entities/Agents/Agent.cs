using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Base;

namespace Backend.Domain.Entities.Agents
{
    [Table("Agent")]
    public class Agent : Model
    {
        [Key]
        public Guid AgentId { get; set; }
        [Required]
        public Guid TenantId { get; set; }
        public Guid? UserId { get; set; }
        [Required]
        public int AgentTypeId { get; set; }
        public string? Name { get; set; }
        
    }
}
