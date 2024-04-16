using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


namespace Frontend.Web.Services.Products
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;



        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        public async Task<ApiResponse<IEnumerable<ProductDetail>>> GetProducts(string tenantId)
        {
            return await _productRepository.GetProducts(tenantId);
        }

        public async Task<ApiResponse<ProductDetail>> GetProductWithDetails(string tenantId, string productId)
        {
            return await _productRepository.GetProductThumbnail(tenantId, productId);
        }

        public async Task<Product> GetProduct(string tenantId, string productId)
        {
            return await _productRepository.GetProduct(tenantId, productId);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            return (await _productRepository.CreateProduct(product)).Result;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }
        public async Task<bool> DeleteProduct(string tenantId, string productId)
        {
            return await _productRepository.DeleteProduct(tenantId, productId);
        }
    }
}
