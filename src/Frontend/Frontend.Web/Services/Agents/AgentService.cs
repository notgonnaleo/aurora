using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Products;
using Frontend.Web.Repository.Agents;
using Frontend.Web.Services.Products;


namespace Frontend.Web.Services.Agents
{
    public class AgentService
    {
        private readonly AgentRepository _agentRepository;

        public AgentService(AgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<IEnumerable<Agent>> GetAgents(string tenantId)
        {
            return await _agentRepository.GetAgents(tenantId);
        }

        public async Task<Agent> CreateAgent(Agent agent)
        {
            return await _agentRepository.CreateAgent(agent);
        }
    }
}
