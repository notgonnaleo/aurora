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

        public async Task<List<Product>> GetProducts(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = ProductsEnums.GET.GetProducts.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Product>().Send(Endpoints.Products, Methods.Default.GET, parameters, null);
            return await _httpClientRepository.Get(request);
        }

        public async Task<Product> GetProduct(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>() // this looks kinda overkill but trust me it's worth the pain
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
            var request = new RouteBuilder<Product>().SendMultiple(Endpoints.Products, Methods.Default.FIND, parameters, null);
            return await _httpClientRepository.GetById(request);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var model = new RouteBuilder<Product>().Send(Endpoints.Products, Methods.Default.POST, null, product);
            return await _httpClientRepository.Post(model);
        }
    }
}
