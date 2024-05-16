using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Frontend.Web.Repository.Products;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Frontend.Web.Services.Products
{
    public class ProductMediaService
    {
        private readonly ProductMediaRepository _productMediaRepository;

        public ProductMediaService(ProductMediaRepository productMediaRepository)
        {
            _productMediaRepository = productMediaRepository;

        }

        public async Task<ApiResponse<ProductMedia>> CreateProductMedia(ProductMedia productmedia)
        {
            return await _productMediaRepository.CreateProductMedia(productmedia);
        }
    }
}
