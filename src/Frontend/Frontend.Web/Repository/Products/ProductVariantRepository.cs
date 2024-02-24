using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;
using static Backend.Infrastructure.Enums.Modules.Methods;
using static Backend.Infrastructure.Enums.Modules.Methods.ProductVariants;

namespace Frontend.Web.Repository.Products
{
    public class ProductVariantRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductVariantRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<IEnumerable<ProductVariant>> GetAllVariantsByProduct(string tenantId, string productId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = GET.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = GET.productId,
                    ParameterValue = productId,
                }
            };
            var request = new RouteBuilder<ProductVariant>().SendMultiple(Endpoints.ProductVariants, GET.GetAllVariantsByProduct.GetAllVariantByProduct, parameters);
            return await _httpClientRepository.Get(request);
        }
        public async Task<ProductVariant> CreateProductVariant(ProductVariant variant)
        {
            var model = new RouteBuilder<ProductVariant>().Send(Endpoints.ProductVariants, Methods.Default.POST, variant);
            var response = await _httpClientRepository.Post(model);
            return await response.Content.ReadFromJsonAsync<ProductVariant>();
        }
    }
}
