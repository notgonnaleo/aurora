using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using System.Net.Http.Json;

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
        public async Task<IEnumerable<Category>> GetCategoriesAndSubCategories(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Categories.GetCategoryAndSubCategoriesParameters.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Categories.GetCategoryAndSubCategories, parameters);
            return await _httpClientRepository.Get(request);
        }
        public async Task<Category> CreateCategory(Category category)
        {
            var model = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Default.POST, category);
            var response = await _httpClientRepository.Post(model);
            return await response.Content.ReadFromJsonAsync<Category>();
        }
    }
}
