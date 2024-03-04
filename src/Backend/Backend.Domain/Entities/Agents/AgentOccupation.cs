using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agents
{
    public class AgentOccupation
    {
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
        public AgentOccupation()
        {
        }
        public AgentOccupation(int id, string name)
        {
            OccupationId = id;
            OccupationName = name;
        }
    }
}
