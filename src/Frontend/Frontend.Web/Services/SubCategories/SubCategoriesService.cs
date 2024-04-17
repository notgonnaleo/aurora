using Backend.Domain.Entities.SubCategories;
using Frontend.Web.Models.Client;
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
            return (await _subCategoryRepository.GetSubCategoriesByCategory(tenantId, categoryId)).Result;
        }
        public async Task<ApiResponse<SubCategory>> CreateSubCategory(SubCategory subCategory)
        {
            return await _subCategoryRepository.CreateSubCategory(subCategory);
        }
        public async Task<ApiResponse<bool>> UpdateSubCategory(SubCategory subCategory)
        {
            var response = await _subCategoryRepository.UpdateSubCategory(subCategory);
            return new ApiResponse<bool>()
            {
                Success = response.Success,
                ResultBoolean = response.ResultBoolean,
                ErrorMessage = response.ErrorMessage,
                StatusCode = response.StatusCode
            };
        }
        public async Task<bool> DeleteSubCategory(string tenantId, string categoryId, string subCategoryId)
        {
            return await _subCategoryRepository.DeleteSubCategory(tenantId, categoryId, subCategoryId);
        }
    }
}
