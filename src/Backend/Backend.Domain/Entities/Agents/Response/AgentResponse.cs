using Backend.Domain.Enums.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agents.Response
{
    public class AgentThumbnailResponse
    {
        public Guid? AgentId { get; set; }
        public string AgentDisplayName { get; set; }
    }

    public class CustomerThumbnail : AgentThumbnailResponse
    {
        public AgentType AgentType { get; set; } = new AgentType() { AgentTypeId = (int)AgentTypes.Customer, AgentTypeName = (AgentTypes.Customer).ToString() }; 
    }

    public class EmployeeThumbnail : AgentThumbnailResponse
    {
        public AgentType AgentType { get; set; } = new AgentType() { AgentTypeId = (int)AgentTypes.Employee, AgentTypeName = (AgentTypes.Employee).ToString() };
    }
}
