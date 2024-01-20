using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.SubCategories
{
    [ApiController]
    [Route("SubCategories")]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryService _SubCategorieservice;

        public SubCategoryController(SubCategoryService SubCategorieservice)
        {
            _SubCategorieservice = SubCategorieservice;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get(Guid tenantId)
        {
            try
            {
                return Ok(_SubCategorieservice.Get(tenantId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid subcategoryId, Guid tenantId)
        {
            try
            {
                return Ok(await _SubCategorieservice.GetById(subcategoryId,tenantId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(SubCategory subCategory)
        {
            try
            {
                return Ok(await _SubCategorieservice.Add(subCategory));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(SubCategory category)
        {
            try
            {
                var SubCategory = await _SubCategorieservice.Update(category);
                return Ok(SubCategory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetSubCategoriesByCategory")]
        public async Task<ActionResult> GetSubCategoriesByCategory(Guid tenantId, Guid categoryId)
        {
            try
            {
                return Ok(await _SubCategorieservice.GetSubCategoriesByCategory(tenantId, categoryId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
