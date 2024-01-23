
using Backend.Domain.Entities.Agent;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

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
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Agents.GET.GetAgents.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Agent>().Send(Endpoints.Agents, Methods.Default.GET, parameters);
            return await _httpClientRepository.Get(request);
        }
    }
}
