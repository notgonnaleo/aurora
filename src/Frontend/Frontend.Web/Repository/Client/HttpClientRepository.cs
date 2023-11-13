using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Frontend.Web.Models.Client;
using Frontend.Web.Util.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net;
using HttpRequestHeader = Frontend.Web.Models.Client.HttpRequestHeader;
using Frontend.Web.Util.Enums.ContentTypeEnums;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Microsoft.AspNetCore.Routing;

namespace Frontend.Web.Repository.Client
{
    public class HttpClientRepository
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestHeader _httpRequestHeader;
        public HttpClientRepository(HttpClient httpClient, HttpRequestHeader httpRequestHeader)
        {
            _httpClient = httpClient;
            _httpRequestHeader = httpRequestHeader;
        }

        /* Private Methods */
        /// <summary>
        /// Get method to list all items.
        /// </summary>
        /// <typeparam name="T">Type of return</typeparam>
        /// <param name="key">Tenant Id</param>
        /// <returns>Returns a list of T</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<T>> Get<T>(RouteBuilder<T> route)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Get, false, ContentTypeEnum.JSON);
            _httpClient.DefaultRequestHeaders.Authorization = httpRequestHeader.Authorization;
            return await _httpClient.GetFromJsonAsync<List<T>>(_httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
        }

        /// <summary>
        /// Get method to list all items.
        /// </summary>
        /// <typeparam name="T">Type of return</typeparam>
        /// <param name="key">Tenant Id</param>
        /// <returns>Returns a list of T</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> GetById<T>(RouteBuilder<T> route)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Get, false, ContentTypeEnum.JSON);
            _httpClient.DefaultRequestHeaders.Authorization = httpRequestHeader.Authorization;
            return await _httpClient.GetFromJsonAsync<T>(_httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
        }

        /// <summary>
        /// Post method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<T> Post<T>(RouteBuilder<T> route)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, false, ContentTypeEnum.JSON);
            var request = new HttpRequestMessage(httpRequestHeader.Method, _httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
            request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                T responseObject = JsonSerializer.Deserialize<T>(responseContent)!;
                return responseObject;
            }
            else
            {
                throw new Exception("HTTP request failed with status code: " + response.StatusCode);
            }
        }

        /// <summary>
        /// Put method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Put<T>(RouteBuilder<T> route)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Put, false, ContentTypeEnum.JSON);
            var endpoint = _httpRequestHeader.BuildRequestUri(httpRequestHeader, route);
            var request = new HttpRequestMessage(httpRequestHeader.Method, endpoint);
            request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, request.Content);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                bool responseObject = JsonSerializer.Deserialize<bool>(responseContent)!;
                return responseObject;
            }
            else
            {
                throw new Exception("HTTP request failed with status code: " + response.StatusCode);
            }
        }

        /* Public Methods */
        /// <summary>
        /// Post method, public not necessary to authenticate first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="isPublic"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post<T>(RouteBuilder<T> route, bool isPublic = true)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, isPublic, ContentTypeEnum.JSON);
            var request = new HttpRequestMessage(httpRequestHeader.Method, $"{httpRequestHeader.Uri}/{route.Endpoint}/{route.ActionName}");
            request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            return await _httpClient.SendAsync(request);
        }
    }
}
