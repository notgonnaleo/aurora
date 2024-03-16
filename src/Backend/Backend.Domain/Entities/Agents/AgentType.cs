using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agents
{
    [Table("AgentType")]
    public class AgentType
    {
        [Key]
        public int AgentTypeId { get; set; }
        public string AgentTypeName { get; set; }
    }
}
