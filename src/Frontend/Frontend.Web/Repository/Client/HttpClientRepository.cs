﻿using Backend.Domain.Entities.Authentication.Users.UserContext;
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
using JsonException = Newtonsoft.Json.JsonException;

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
        public async Task<ApiResponse<IEnumerable<T>>> Get<T>(RouteBuilder<T> route)
        {
            try
            {
                HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Get, false, ContentTypeEnum.JSON);
                _httpClient.DefaultRequestHeaders.Authorization = httpRequestHeader.Authorization;
                HttpResponseMessage response = await _httpClient.GetAsync(_httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<T> result = JsonConvert.DeserializeObject<List<T>>(responseContent);
                    return new ApiResponse<IEnumerable<T>>()
                    {
                        StatusCode = 200,
                        ErrorMessage = null,
                        Result = result,
                        Success = true,
                    };
                }
                return new ApiResponse<IEnumerable<T>>
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = "Error: " + response.ReasonPhrase,
                    Result = null,
                    Success = false,
                };
            }
            catch (Exception ApiException)
            {
                return new ApiResponse<IEnumerable<T>>()
                {
                    StatusCode = 500,
                    ErrorMessage = ApiException.Message,
                    Result = null,
                    Success = false,
                };
            }
        }

        /// <summary>
        /// Get method to list all items.
        /// </summary>
        /// <typeparam name="T">Type of return</typeparam>
        /// <param name="key">Tenant Id</param>
        /// <returns>Returns a list of T</returns>
        /// <exception cref="ApiResponse<T>">404, 500</exception>
        public async Task<ApiResponse<T>> Find<T>(RouteBuilder<T> route)
        {
            try
            {
                HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Get, false, ContentTypeEnum.JSON);
                _httpClient.DefaultRequestHeaders.Authorization = httpRequestHeader.Authorization;
                HttpResponseMessage response = await _httpClient.GetAsync(_httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
                
                if(response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>()
                    {
                        StatusCode = 200,
                        ErrorMessage = null,
                        Result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()),
                        Success = true,
                    };
                }

                return new ApiResponse<T>()
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = await response.Content.ReadAsStringAsync(),
                    Result = default(T),
                    Success = false,
                };
            }
            catch (Exception ApiException)
            {
                return new ApiResponse<T>()
                {
                    StatusCode = 500,
                    ErrorMessage = ApiException.Message,
                    Result = default,
                    Success = false,
                };
            }
        }

        /// <summary>
        /// Post method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TResponse>> Post<TRequest, TResponse>(RouteBuilder<TRequest> route)
        {
            try
            {
                HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, false, ContentTypeEnum.JSON);
                var uri = _httpRequestHeader.BuildRequestUri(httpRequestHeader, route);
                var request = new HttpRequestMessage(httpRequestHeader.Method, uri);
                if (route.Body != null)
                    request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var deserializedData = JsonConvert.DeserializeObject<TResponse>(data);
                    return new ApiResponse<TResponse>()
                    {
                        StatusCode = 200,
                        ErrorMessage = null,
                        Result = deserializedData,
                        Success = true,
                    };
 
                }

                return new ApiResponse<TResponse>()
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = await response.Content.ReadAsStringAsync(),
                    Result = default(TResponse),
                    Success = false,
                };
            }
            catch (Exception ApiException)
            {
                return new ApiResponse<TResponse>()
                {
                    StatusCode = 500,
                    ErrorMessage = ApiException.Message,
                    Result = default,
                    Success = false,
                };
            }
        }

        /// <summary>
        /// Put method, private only with authorization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> Put<T>(RouteBuilder<T> route)
        {
            try
            {
                HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Put, false, ContentTypeEnum.JSON);
                var uri = _httpRequestHeader.BuildRequestUri(httpRequestHeader, route);
                var request = new HttpRequestMessage(httpRequestHeader.Method, uri);
                if (route.Body != null)
                    request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var deserializedData = JsonConvert.DeserializeObject<T>(data);
                        return new ApiResponse<T>()
                        {
                            StatusCode = 200,
                            ErrorMessage = null,
                            Result = deserializedData,
                            Success = true,
                        };
                    }
                    catch (JsonException)
                    {
                        // For boolean API responses.
                        return new ApiResponse<T>()
                        {
                            StatusCode = 200,
                            ErrorMessage = null,
                            Result = default(T),
                            ResultBoolean = JsonConvert.DeserializeObject<bool>(data),
                            Success = true,
                        };
                    }
                }

                return new ApiResponse<T>()
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = await response.Content.ReadAsStringAsync(),
                    Result = default(T),
                    Success = false,
                };
            }
            catch (Exception ApiException)
            {
                return new ApiResponse<T>()
                {
                    StatusCode = 500,
                    ErrorMessage = ApiException.Message,
                    Result = default,
                    Success = false,
                };
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
            try
            {
                HttpRequestHeader httpRequestHeader = await _httpRequestHeader.BuildHttpRequestHeader(HttpMethod.Post, isPublic, ContentTypeEnum.JSON);
                var request = new HttpRequestMessage(httpRequestHeader.Method, _httpRequestHeader.BuildRequestUri(httpRequestHeader, route));
                request.Content = new StringContent(JsonSerializer.Serialize(route.Body), httpRequestHeader.Encoding, httpRequestHeader.ContentType);
                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
