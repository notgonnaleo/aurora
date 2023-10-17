using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

namespace Frontend.Web.Repository.Authentication
{
    public class AuthenticationRepository : HttpClientRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public AuthenticationRepository(HttpClient httpClient, HttpRequestHeader httpRequestHeader, HttpClientRepository httpClientRepository) : base(httpClient, httpRequestHeader)
        {
            _httpClientRepository = httpClientRepository;
        }
        public async Task<bool> Sign(string tenantId)
        {
            RouteBuilder route = new RouteBuilder().BuildRoute(Endpoints.Authentication, Methods.GET, $"tenantId={tenantId}", null);
            var result = await _httpClientRepository.Post<UserSessionContext>(route);
        }
    }
}
