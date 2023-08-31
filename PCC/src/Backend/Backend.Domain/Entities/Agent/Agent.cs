using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agent
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CNAEId { get; set; }
    }
}
