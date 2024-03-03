using Backend.Domain.Entities.Agents;
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

        public async Task<IEnumerable<Backend.Domain.Entities.Agents.Agent>> GetAgents(string tenantId)
        {
            return await _agentRepository.GetAgents(tenantId);
        }

        public async Task<Backend.Domain.Entities.Agents.Agent> GetAgent(string tenantId, string agentId)
        {
            return await _agentRepository.GetAgent(tenantId, agentId);
        }

        public async Task<Backend.Domain.Entities.Agents.Agent> CreateAgent(Backend.Domain.Entities.Agents.Agent agent)
        {
            return await _agentRepository.CreateAgent(agent);
        }

        public async Task<bool> UpdateAgent(Backend.Domain.Entities.Agents.Agent agent)
        {
            return await _agentRepository.UpdateAgent(agent);
        }
    }
}
