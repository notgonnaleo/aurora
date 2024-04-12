using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;
using ProductsEnums = Backend.Infrastructure.Enums.Modules.Methods.Products;

namespace Frontend.Web.Services.Products
{
    public class ProductRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<IEnumerable<ProductDetail>> GetProducts(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = ProductsEnums.GET.GetProducts.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<ProductDetail>().Send(Endpoints.Products, Methods.Default.GET, parameters);
            return await _httpClientRepository.Get(request);
        }

        public async Task<Product> GetProduct(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = ProductsEnums.GET.GetProduct.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = ProductsEnums.GET.GetProduct.productId,
                    ParameterValue = productId,
                }
            };
            var request = new RouteBuilder<Product>().SendMultiple(Endpoints.Products, Methods.Default.FIND, parameters);
            return await _httpClientRepository.GetById(request);
        }
        public async Task<ProductDetail> GetProductThumbnail(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = ProductsEnums.GET.GetProduct.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = ProductsEnums.GET.GetProduct.productId,
                    ParameterValue = productId,
                }
            };
            var request = new RouteBuilder<ProductDetail>().SendMultiple(Endpoints.Products, Methods.Default.FIND, parameters);
            return await _httpClientRepository.GetById(request);
        }
        public async Task<Product> CreateProduct(Product product)
        {
            var model = new RouteBuilder<Product>().Send(Endpoints.Products, Methods.Default.POST, product);
            var response = await _httpClientRepository.Post(model);
            return await response.Content.ReadFromJsonAsync<Product>();
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var model = new RouteBuilder<Product>().Send(Endpoints.Products, Methods.Default.PUT, product);
            return await _httpClientRepository.Put(model);
        }
        public async Task<bool> DeleteProduct(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>()
                {
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Products.DELETE.DeleteProduct.tenantId,
                        ParameterValue = tenantId,
                    },
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.Products.DELETE.DeleteProduct.productId,
                        ParameterValue = productId,
                    }
                };
            var request = new RouteBuilder<Product>().SendMultiple(Endpoints.Products, Methods.Default.DELETE, parameters);
            return await _httpClientRepository.Put(request);
        }
    }
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
}
