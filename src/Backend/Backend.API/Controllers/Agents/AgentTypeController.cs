using Backend.Domain.Entities.Agents;
using Backend.Infrastructure.Services.Agents;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Agents
{
    [ApiController]
    [Route("AgentTypes")]
    public class AgentTypeController : ControllerBase
    {
        private readonly AgentTypeService _agentTypeService;
        public AgentTypeController(AgentTypeService agentTypeService)
        {
            _agentTypeService = agentTypeService;
        }

        [HttpGet]
        [Route("List")]
        public IEnumerable<AgentType> GetAgentTypes()
        {
            return _agentTypeService.GetAgentTypes();
        }
    }
}
