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

        public async Task<IEnumerable<Agent>> GetAgents(string tenantId)
        {
            return await _agentRepository.GetAgents(tenantId);
        }

        public async Task<IEnumerable<AgentType>> GetAgentTypes()
        {
            return await _agentRepository.GetAgentTypes();
        }

        public async Task<Agent> GetAgent(string tenantId, string agentId)
        {
            return await _agentRepository.GetAgent(tenantId, agentId);
        }
        public async Task<AgentDetail> GetAgentWithDetail(string tenantId, string agentId)
        {
            return new AgentDetail();
        }

        public async Task<Agent> CreateAgent(Agent agent)
        {
            return await _agentRepository.CreateAgent(agent);
        }

        public async Task<bool> UpdateAgent(Agent agent)
        {
            return await _agentRepository.UpdateAgent(agent);
        }
    }
}
