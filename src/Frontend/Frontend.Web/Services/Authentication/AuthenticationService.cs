using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Frontend.Web.Util.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace Frontend.Web.Services.Authentication
{
    public class AuthenticationService
    {
        private readonly HttpClientRepository _httpClientRepository;
        private readonly SessionStorageAccessor _sessionStorageAccessor;
        public AuthenticationService(HttpClientRepository httpClientRepository, SessionStorageAccessor sessionStorageAccessor)
        {
            _sessionStorageAccessor = sessionStorageAccessor;
            _httpClientRepository = httpClientRepository;
        }
        public async Task<bool> SignIn(LoginRequest model)
        {
            RouteBuilder<LoginRequest> routeBuilder = new RouteBuilder<LoginRequest>().BuildRoute(Endpoints.Authentication, Methods.Authentication.Login, string.Empty, model);
            var response = await _httpClientRepository.Post(routeBuilder, true);

            var userSession = await response.Content.ReadFromJsonAsync<UserSessionContext>();
            await _sessionStorageAccessor.SetValueAsync("UserSession", JsonSerializer.Serialize(userSession));
            if (userSession == null)
                throw new Exception("Error while trying to log in");
            return userSession.Success;
        }
    }
}
