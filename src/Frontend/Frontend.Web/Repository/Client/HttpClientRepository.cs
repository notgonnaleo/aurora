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
        public async Task<List<T>> Get<T>(string key)
        {
            // I think we can encapsulate this more hehehe
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Get, false, ContentTypeEnum.JSON);
            _httpClient.DefaultRequestHeaders.Authorization = httpRequestHeader.Authorization;
            return await _httpClient.GetFromJsonAsync<List<T>>($"{httpRequestHeader.Endpoint}/Products/List?tenantId=" + key);
        }

        /// <summary>
        /// Post method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post<T>(T model)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, false, ContentTypeEnum.JSON);
            var request = new HttpRequestMessage(httpRequestHeader.Method, $"{httpRequestHeader.Endpoint}/Authentication/Login");
            request.Content = new StringContent(JsonSerializer.Serialize(model), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            return await _httpClient.SendAsync(request);
        }

        /// <summary>
        /// Put method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Put<T>(T model)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Put, false, ContentTypeEnum.JSON);
            var request = new HttpRequestMessage(httpRequestHeader.Method, $"{httpRequestHeader.Endpoint}/RouteGoHere");
            request.Content = new StringContent(JsonSerializer.Serialize(model), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            return await _httpClient.SendAsync(request);
        }

        /// <summary>
        /// Delete method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<HttpRequestMessage> Delete<T>(Guid key1) // review
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Put, false, ContentTypeEnum.JSON);
            return new HttpRequestMessage(httpRequestHeader.Method, $"{httpRequestHeader.Endpoint}/Products/Delete?Id={key1}");
            
        }

        /* Public Methods */
        /// <summary>
        /// Post method, public not necessary to authenticate first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="isPublic"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post<T>(T model, bool isPublic = true)
        {
            HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, true, ContentTypeEnum.JSON);
            var request = new HttpRequestMessage(httpRequestHeader.Method, $"{httpRequestHeader.Endpoint}/Authentication/Login");
            request.Content = new StringContent(JsonSerializer.Serialize(model), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
            return await _httpClient.SendAsync(request);
        }
    }
}
