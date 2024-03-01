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
    public class Email
    {
        [Key]
        public Guid EmailAddressId { get; set; }
        public Guid TenantId { get; set; }
        public Guid AgentId { get; set; }

        public string EmailAddress { get; set; }
        public bool Primary { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }
    }
}
