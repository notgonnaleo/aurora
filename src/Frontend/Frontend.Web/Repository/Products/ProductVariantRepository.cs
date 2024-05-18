using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
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

        public async Task<ApiResponse<IEnumerable<ProductVariant>>> GetAllVariantsByProduct(string tenantId, string productId)
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
            var request = new RouteBuilder<ProductVariant>().SendMultiple(Endpoints.ProductVariants, GET.GetAllVariantsByProduct.GetAllVariantsByProductEndpoint, parameters);
            return await _httpClientRepository.Get(request);
        }



        public async Task<ApiResponse<ProductVariant>> CreateProductVariant(ProductVariant variant)
        {
            var model = new RouteBuilder<ProductVariant>().Send(Endpoints.ProductVariants, Methods.Default.POST, variant);
            return await _httpClientRepository.Post<ProductVariant, ProductVariant>(model);
        }

        public async Task<ApiResponse<ProductVariant>> UpdateProductVariant(ProductVariant variant)
        {
            var model = new RouteBuilder<ProductVariant>().Send(Endpoints.ProductVariants, Methods.Default.PUT, variant);
            return await _httpClientRepository.Put(model);
        }

        public async Task<bool> DeleteVariant(string tenantId, string variantId)
        {
            var parameters = new List<RouteParameterRequest>()
                {
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.ProductVariants.DELETE.DeleteVariant.tenantId,
                        ParameterValue = tenantId,
                    },
                    new RouteParameterRequest()
                    {
                        ParameterName = Methods.ProductVariants.DELETE.DeleteVariant.variantId,
                        ParameterValue = variantId,
                    }
                };
            var request = new RouteBuilder<ProductVariant>().SendMultiple(Endpoints.ProductVariants, Methods.Default.DELETE, parameters);
            return (await _httpClientRepository.Put(request)).Success;
        }
    }
}
