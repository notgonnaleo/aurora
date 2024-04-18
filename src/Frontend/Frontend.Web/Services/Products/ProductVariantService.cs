using Backend.Domain.Entities.Products;
using Frontend.Web.Models.Client;
using Frontend.Web.Repository.Products;

namespace Frontend.Web.Services.Products
{
    public class ProductVariantService
    {
        private readonly ProductVariantRepository _productVariantRepository;
        public ProductVariantService(ProductVariantRepository productVariantRepository)
        {
            _productVariantRepository = productVariantRepository;
        }

        public async Task<ApiResponse<IEnumerable<ProductVariant>>> GetVariantsByProduct(string tenantId, string productId)
        {
            return await _productVariantRepository.GetAllVariantsByProduct(tenantId, productId);
        }

        public async Task<ApiResponse<ProductVariant>> CreateVariant(ProductVariant productVariant)
        {
            return await _productVariantRepository.CreateProductVariant(productVariant);
        }
    }
} 