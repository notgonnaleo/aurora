using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

using ProductsEnums = Backend.Infrastructure.Enums.Modules.Methods.Products;

namespace Frontend.Web.Services.Products
{
    public class ProductService
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductService(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public RouteBuilder<Product> Send(RouteParameterRequest parameters)
        {
            return new RouteBuilder<Product>()
                .BuildRoute(Endpoints.Products,
                Methods.Default.GET,
                new RouteParameters().BuildParameterString(new RouteParameters()
                {
                    Key = 1, // whatever
                    ParameterName = parameters.ParameterName,
                    ParameterValue = parameters.ParameterValue, // Right now i will treat everyone as a string, in the future i will make this type adaptable
                }),
                null);
        }

        public RouteBuilder<Product> SendMultiple(List<RouteParameterRequest> parameters)
        {
            int key = 0;
            var parameterRequestList = new List<RouteParameters>();

            foreach (var parameter in parameters)
            {
                parameterRequestList.Add(new RouteParameters() 
                { 
                    Key = key,
                    ParameterName = parameter.ParameterName,
                    ParameterValue = parameter.ParameterValue
                });
                key++;
            }
            return new RouteBuilder<Product>()
                .BuildRoute(Endpoints.Products, 
                Methods.Default.FIND, 
                new RouteParameters().BuildParameterString(parameterRequestList), 
                null);
        }

        public async Task<List<Product>> GetProducts(string tenantId)
        {
            var request = Send(new RouteParameterRequest() { ParameterName = ProductsEnums.GET.GetProducts.tenantId, ParameterValue = tenantId });
            return await _httpClientRepository.Get(request);
        }

        public async Task<Product> GetProduct(string tenantId, string productId)
        {
            var request = SendMultiple(new List<RouteParameterRequest>() 
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
            });

            return await _httpClientRepository.GetById(request);
        }
    }
}
