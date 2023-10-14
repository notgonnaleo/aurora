using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
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
            _httpClientRepository = httpClientRepository;
            _sessionStorageAccessor = sessionStorageAccessor;
        }
        public async Task<bool> SignIn(LoginRequest model)
        {
            var response = await _httpClientRepository.Post(model, true);
            var userSession = await response.Content.ReadFromJsonAsync<UserSessionContext>();
            await _sessionStorageAccessor.SetValueAsync("UserSession", JsonSerializer.Serialize(userSession));
            if (userSession == null)
                throw new Exception("Error while trying to log in");
            return userSession.Success;
        }
    }
}
