using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Authentication;
using Frontend.Web.Repository.Client;
using Frontend.Web.Services.Tenants;
using Frontend.Web.Util.Cookie;
using Frontend.Web.Util.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using System.Transactions;

namespace Frontend.Web.Services.Authentication
{
    public class AuthenticationService
    {
        private readonly HttpClientRepository _httpClientRepository;
        private readonly CookieHandler _cookies;
        private readonly AuthenticationRepository _authenticationRepository;

        public AuthenticationService(HttpClientRepository httpClientRepository, CookieHandler cookies, AuthenticationRepository authenticationRepository)
        {
            _cookies = cookies;
            _httpClientRepository = httpClientRepository;
            _authenticationRepository = authenticationRepository;
        }
        public async Task<bool> SignIn(LoginRequest model)
        {
            var response = await _authenticationRepository.SignIn(model);
            await _cookies.SetValueAsync("UserSession", JsonSerializer.Serialize(response));
            return response != null;
        }

        public async Task<ApiResponse<bool>> Validate()
        {
            var response = await _authenticationRepository.Validate();
            if(response.StatusCode == 404)
            {
                if (await _cookies.GetValueAsync<UserSessionContext>("UserSession") is not null)
                    await _cookies.Clear("UserSession");
                return response;
            }
            return response;
        }

        public async Task<ApiResponse<bool>?> IsUserLogged()
        {
            var isValid = await Validate();
            return isValid;
        }

        public async Task<UserSessionContext?> GetContext()
        {
            var response = await _cookies.GetValueAsync<UserSessionContext>("UserSession");
            return response;
        }

        public async Task<bool> UpdateTenantInformationInContext(Tenant selectedTenant)
        {
            UserSessionContext? context = await GetContext();
            if(context == null) 
                return false;
            context.Tenant = selectedTenant;
            await _cookies.SetValueAsync("UserSession", JsonSerializer.Serialize(context));
            return true;
        }
    }
}
