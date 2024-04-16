using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Enums.Modules;
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

        public async Task<bool> Validate()
        {
            var response = await _authenticationRepository.Validate();
            if(response.Token is null)
            {
                if (await _cookies.GetValueAsync<UserSessionContext>("UserSession") is not null)
                {
                    await _cookies.Clear("UserSession");
                    return true;
                }
            }
            return true;
        }

        /*
         * [Informação importante sobre as atualizações no modulo de autenticação]
         * 
         * TODO: Existe um bug causado por conta do uso de cookies em que quando o servidor morre
         * E o usuário acessa o site com o mesmo token utilizado no login anterior antes da morte do server
         * o servidor vai retornar 401 porém no frontend só estamos validando se os cookies estão preenchidos
         * precisa-se adicionar uma nova feature na qual:
         *
         * Frontend valida: 
         * #1 - Cookie da UserSession existe?
         * #2 - Token salvo no frontend (JSON de reposta salvo após o login) também existe na memória do servidor?
         * 
         * Resposta N1:
         * Se #2 for verdade então usuário pode prosseguir para a tela de Dashboard
         * Caso falso, retorne o usuário para a tela de login aonde ele deve re-logar 
         * 
         * Requisitos N1:
         * Endpoint para checkar no cache do servidor se o token e a session correspondem
         * dependendo do retorno, alterar o comportamente realizado no metodo IsUserLogged
         * 
         * OU
         * 
         * Reposta N2:
         * Se #2 for verdade, executar refresh no token e nos dados da sessão do usuário,
         * 
         * Requisitos N2:
         * Provavelmente, adicionar dados validos dentro da estrutura payload do JWT como 
         * ID do Usuario 
         * Data e Hora em que o Login foi realizado
         * Booleano para se o usuário gostaria de manter a sessão persistente ("Lembrar minha senha")
         */
        public async Task<bool?> IsUserLogged()
        {
            return await _cookies.GetValueAsync<UserSessionContext>("UserSession") != null;
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
