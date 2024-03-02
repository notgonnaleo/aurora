using Backend.Domain.Entities.Agents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Contacts
{
    public class Phone
    {
        [Key]
        public Guid PhoneId { get; set; }
        public Guid TenantId { get; set; }
        public Guid AgentId { get; set; }

        public string? AreaCode { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Primary { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }
    }
}
