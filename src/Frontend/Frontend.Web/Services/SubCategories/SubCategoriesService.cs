using Backend.Domain.Entities.SubCategories;
using Frontend.Web.Repository.SubCategories;

namespace Frontend.Web.Services.SubCategories
{
    public class SubCategoryService
    {
        private readonly SubCategoryRepository _subCategoryRepository;
        public SubCategoryService(SubCategoryRepository subCategoryRepository) 
        { 
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryId(string tenantId, string categoryId)
        {
            var result = await _subCategoryRepository.GetSubCategoriesByCategory(tenantId, categoryId);
            return result;
        }
        public async Task<SubCategory> CreateSubCategory(SubCategory subCategory)
        {
            return await _subCategoryRepository.CreateSubCategory(subCategory);
        }
        public async Task<bool> UpdateSubCategory(SubCategory subCategory)
        {
            return await _subCategoryRepository.UpdateSubCategory(subCategory);
        }
    }
}
