using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

namespace Frontend.Web.Repository.Authentication
{
    public class AuthenticationRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public AuthenticationRepository(HttpClientRepository httpClientRepository) 
        { 
            _httpClientRepository = httpClientRepository;
        }
        public async Task<UserSessionContext> SignIn(LoginRequest model)
        {
            RouteBuilder<LoginRequest> routeBuilder = new RouteBuilder<LoginRequest>().Send(Endpoints.Authentication, Methods.Authentication.Login, model);
            var response = await _httpClientRepository.Post(routeBuilder, true);
            return await response.Content.ReadFromJsonAsync<UserSessionContext>();
        }

        public async Task<UserSessionContext> Validate()
        {
            RouteBuilder<UserSessionContext> routeBuilder = new RouteBuilder<UserSessionContext>().Send(Endpoints.Authentication, Methods.Authentication.Validate);
            return await _httpClientRepository.GetById(routeBuilder);
        }
    }
}
