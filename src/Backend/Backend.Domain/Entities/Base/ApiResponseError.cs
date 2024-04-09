using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Base
{
    public class ApiResponseError
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        [JsonProperty("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }

        // Constructor to initialize the dictionary
        public ApiResponseError()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        // Method to add errors
        public void AddError(string key, string message)
        {
            if (Errors.ContainsKey(key))
            {
                Errors[key].Add(message);
            }
            else
            {
                Errors.Add(key, new List<string> { message });
            }
        }

        // Method to print all errors (for debugging or logging)
        public void PrintErrors()
        {
            foreach (var error in Errors)
            {
                Console.WriteLine($"{error.Key}: {string.Join(", ", error.Value)}");
            }
        }
    }

    // Example of an inherited class for a specific error scenario
    public class ProductValidationError : ApiResponseError
    {
        // You can add additional properties or methods specific to the product validation error scenario here
        public ProductValidationError() : base()
        {
            this.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            this.Title = "One or more validation errors occurred.";
        }

        // Add any additional specialized methods for handling product validation errors here
    }


}
