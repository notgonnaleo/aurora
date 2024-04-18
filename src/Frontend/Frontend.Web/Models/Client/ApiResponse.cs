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
        public bool ResultBoolean { get; set; } // For now it's being used when the API response is boolean but the endpoint requires a type.
        public bool Success { get; set; }

        public bool Successful()
        {
            return (Result is not null || ResultBoolean) && Success;
        }
    }
}
