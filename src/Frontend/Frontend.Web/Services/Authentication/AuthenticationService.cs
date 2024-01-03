using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Authentication;
using Frontend.Web.Repository.Client;
using Frontend.Web.Services.Tenants;
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
        private readonly AuthenticationRepository _authenticationRepository;

        public AuthenticationService(HttpClientRepository httpClientRepository, SessionStorageAccessor sessionStorageAccessor, AuthenticationRepository authenticationRepository)
        {
            _sessionStorageAccessor = sessionStorageAccessor;
            _httpClientRepository = httpClientRepository;
            _authenticationRepository = authenticationRepository;
        }
        public async Task<bool> SignIn(LoginRequest model)
        {
            var response = await _authenticationRepository.SignIn(model);
            await _sessionStorageAccessor.SetValueAsync("UserSession", JsonSerializer.Serialize(response));
            return response != null;
        }

        public async Task<bool> IsUserLogged()
        {
            var session = await _sessionStorageAccessor.GetValueAsync<UserSessionContext>("UserSession");
            if(session != null)
                return true;

            return false;
        }

        public async Task<UserSessionContext?> GetContext()
        {
            return await _sessionStorageAccessor.GetValueAsync<UserSessionContext>("UserSession");
        }

        public async Task<bool> UpdateTenantInformationInContext(Tenant selectedTenant)
        {
            UserSessionContext context = await GetContext();

            context.Tenant = selectedTenant;
            await _sessionStorageAccessor.SetValueAsync("UserSession", JsonSerializer.Serialize(context));
            return true;
        }
    }
}
