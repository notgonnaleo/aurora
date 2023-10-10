using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Frontend.Web.Models.Client;
using Frontend.Web.Util.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Frontend.Web.Util.Enums.HttpMethodEnums;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Text.Json;

namespace Frontend.Web.Repository.Client
{
    public class HttpClientRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SessionStorageAccessor _sessionProvider;
        private readonly HttpRequestHeader _httpRequestHeader;
        public HttpClientRepository(HttpClient httpClient, IConfiguration configuration, SessionStorageAccessor sessionProvider, HttpRequestHeader httpRequestHeader)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _sessionProvider = sessionProvider;
            _httpRequestHeader = httpRequestHeader;
        }

        /// <summary>
        /// Get method to list all items.
        /// </summary>
        /// <typeparam name="T">Type of return</typeparam>
        /// <param name="key">Tenant Id</param>
        /// <returns>Returns a list of T</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<T>> Get<T>(string key)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader();
            return await _httpClient.GetFromJsonAsync<List<T>>($"{httpRequestHeader.Endpoint}/Products/List?tenantId=" + key);
        }

        public async Task<T> Post<T>()
        {
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7299/Authentication/Login"); // TODO: use environment variables.
            //request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            //var response = await _http.SendAsync(request);

            //var userSession = await response.Content.ReadFromJsonAsync<UserSessionContext>();
            //return userSession;
            throw new NotImplementedException();
        }
    }
}
