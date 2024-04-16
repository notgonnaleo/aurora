using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
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

        public async Task<ApiResponse<Agent>> CreateAgent(Agent agent)
        {
            var model = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.POST, agent);
            return await _httpClientRepository.Post(model);
        }

        public async Task<IEnumerable<Agent>> GetAgents(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = AgentsEnums.GET.GetAgents.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.GET, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }

        public async Task<AgentDetail> GetAgentWithDetail(string agentId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = AgentsEnums.GET.GetAgentWithDetail.Args.agentId, ParameterValue = agentId };
            var request = new RouteBuilder<AgentDetail>().Send(Endpoints.Agents, Methods.Agents.GET.GetAgentWithDetail.RouteName, parameters);
            return (await _httpClientRepository.Find(request)).Result;
        }

        public async Task<IEnumerable<AgentType>> GetAgentTypes()
        {
            var request = new RouteBuilder<AgentType>().Send(Endpoints.AgentTypes, Methods.Default.GET);
            return (await _httpClientRepository.Get(request)).Result;
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
            return (await _httpClientRepository.Find(request)).Result;
        }

        public async Task<bool> UpdateAgent(Agent agent)
        {
            var model = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.PUT, agent);
            return await _httpClientRepository.Put(model);
        }

        public async Task<bool> DeleteAgent(string tenantId, string agentId)
        {
            var parameters = new List<RouteParameterRequest>()
                {
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Agents.DELETE.DeleteAgent.tenantId,
                        ParameterValue = tenantId,
                    },
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Agents.DELETE.DeleteAgent.agentId,
                        ParameterValue = agentId,
                    }
                };
            var request = new RouteBuilder<Agent>().SendMultiple(Endpoints.Agents, Methods.Default.DELETE, parameters);
            return await _httpClientRepository.Put(request);
        }
    }
}
