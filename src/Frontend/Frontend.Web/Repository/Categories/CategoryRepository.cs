using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;

namespace Frontend.Web.Repository.Categories
{
    public class CategoryRepository
    {
        private readonly HttpClientRepository _httpClientRepository;
        public CategoryRepository(HttpClientRepository httpClientRepository) 
        { 
            _httpClientRepository = httpClientRepository;
        }
        public async Task<IEnumerable<Category>> GetCategories(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Categories.GET.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Default.GET, parameters);
            return await _httpClientRepository.Get(request);
        }
    }
}
