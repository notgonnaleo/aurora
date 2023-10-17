using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

namespace Frontend.Web.Services.Products
{
    public class ProductService
    {
        private readonly HttpClientRepository _httpClientRepository;
        public ProductService(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }
    }
}
