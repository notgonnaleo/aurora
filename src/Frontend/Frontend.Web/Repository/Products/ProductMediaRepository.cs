using Backend.Domain.Entities.Authentication.Tenants;
using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Frontend.Web.Repository.Products
{
    public class ProductMediaRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductMediaRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<ApiResponse<ProductMedia>> CreateProductMedia(ProductMedia productmedia)
        {
            var model = new RouteBuilder<ProductMedia>().Send(Endpoints.ProductMedia, Methods.Default.POST, productmedia);
            return await _httpClientRepository.Post<ProductMedia,ProductMedia>(model);
        }

        
    }
}
