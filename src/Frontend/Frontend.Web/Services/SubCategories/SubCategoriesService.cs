using Backend.Domain.Entities.SubCategories;
using Frontend.Web.Repository.SubCategories;

namespace Frontend.Web.Services.SubCategories
{
    public class SubCategoriesService
    {
        private readonly SubCategoryRepository _subCategoryRepository;
        public SubCategoriesService(SubCategoryRepository subCategoryRepository) 
        { 
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryId(string tenantId, string categoryId)
        {
            var result = await _subCategoryRepository.GetSubCategoriesByCategory(tenantId, categoryId);
            return result;
        }
    }
}
