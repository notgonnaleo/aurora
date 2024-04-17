using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Enums.Modules;
using Frontend.Web.Models.Client;
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
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Categories.GET.GetCategories.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Default.GET, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }
        public async Task<Category> GetCategory(string tenantId, string categoryId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Categories.GET.GetCategory.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Categories.GET.GetCategory.categoryId,
                    ParameterValue = categoryId,
                },
            };
            var request = new RouteBuilder<Category>().SendMultiple(Endpoints.Category, Methods.Default.FIND, parameters);
            return (await _httpClientRepository.Find(request)).Result;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAndSubCategories(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Categories.GET.GetCategoryAndSubCategoriesParameters.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Categories.GET.GetCategoryAndSubCategories, parameters);
            return (await _httpClientRepository.Get(request)).Result;
        }
        public async Task<ApiResponse<Category>> CreateCategory(Category category)
        {
            var model = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Default.POST, category);
            return await _httpClientRepository.Post(model);
        }
        public async Task<ApiResponse<Category>> UpdateCategory(Category category)
        {
            var model = new RouteBuilder<Category>().Send(Endpoints.Category, Methods.Default.PUT, category);
            return await _httpClientRepository.Put(model);
        }
        public async Task<bool> DeleteCategory(string tenantId, string categoryId)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Categories.DELETE.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Categories.DELETE.categoryId,
                    ParameterValue = categoryId,
                },
            };
            var request = new RouteBuilder<Category>().SendMultiple(Endpoints.Category, Methods.Default.DELETE, parameters);
            return (await _httpClientRepository.Put(request)).Success;
        }
    }
}
