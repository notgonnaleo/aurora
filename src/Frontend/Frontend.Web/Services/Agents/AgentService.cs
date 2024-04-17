using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Products;
using Frontend.Web.Models.Client;
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

        public async Task<IEnumerable<AgentType>> GetAgentTypes()
        {
            return await _agentRepository.GetAgentTypes();
        }


        public async Task<Backend.Domain.Entities.Agents.Agent> GetAgent(string tenantId, string agentId)
        {
            return await _agentRepository.GetAgent(tenantId, agentId);
        }
        public async Task<AgentDetail> GetAgentWithDetail(string agentId)
        {
            return await _agentRepository.GetAgentWithDetail(agentId);
        }

        public async Task<ApiResponse<Backend.Domain.Entities.Agents.Agent>> CreateAgent(Backend.Domain.Entities.Agents.Agent agent)
        {
            return await _agentRepository.CreateAgent(agent);
        }

        public async Task<ApiResponse<bool>> UpdateAgent(Backend.Domain.Entities.Agents.Agent agent)
        {
            var response = await _agentRepository.UpdateAgent(agent);
            return new ApiResponse<bool>()
            {
                Success = response.Success,
                ResultBoolean = response.ResultBoolean,
                ErrorMessage = response.ErrorMessage,
                StatusCode = response.StatusCode
            };
        }

        public async Task<bool> DeleteAgent(string tenantId, string agentId)
        {
            return await _agentRepository.DeleteAgent(tenantId, agentId);
        }
    }
}
