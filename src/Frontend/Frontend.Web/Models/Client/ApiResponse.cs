using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Web.Models.Client
{
    public class ApiResponse<T>
    {
        [JsonProperty("status")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
        public bool Success { get; set; }
    }
}
