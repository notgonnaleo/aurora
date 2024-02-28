using Backend.Domain.Entities.Agents;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;
using AgentsEnums = Backend.Infrastructure.Enums.Modules.Methods.Agents;

namespace Frontend.Web.Repository.Agents
{
    public class AgentRepository
    {
        private readonly HttpClientRepository _httpClientRepository;

        public AgentRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<Agent> CreateAgent(Agent agent)
        {
            var model = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.POST, agent);
            var response = await _httpClientRepository.Post(model);
            return await response.Content.ReadFromJsonAsync<Agent>();
        }

        public async Task<IEnumerable<Agent>> GetAgents(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = AgentsEnums.GET.GetAgents.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.GET, parameters);
            return await _httpClientRepository.Get(request);
        }

        public async Task<IEnumerable<AgentType>> GetAgentTypes()
        {
            var request = new RouteBuilder<AgentType>().Send(Endpoints.AgentTypes, Methods.Default.GET);
            return await _httpClientRepository.Get(request);
        }

        public async Task<Agent> GetAgent(string tenantId, string agentId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = AgentsEnums.GET.GetAgent.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = AgentsEnums.GET.GetAgent.agentId,
                    ParameterValue = agentId,
                }
            };
            var request = new RouteBuilder<Agent>().SendMultiple(Endpoints.Agents, Methods.Default.FIND, parameters);
            return await _httpClientRepository.GetById(request);
        }

        public async Task<bool> UpdateAgent(Agent agent)
        {
            var model = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.PUT, agent);
            return await _httpClientRepository.Put(model);
        }
    }
}
