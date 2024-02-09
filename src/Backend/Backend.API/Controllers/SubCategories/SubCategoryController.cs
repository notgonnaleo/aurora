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
        private readonly SubCategoryService _SubCategoriesService;

        public SubCategoryController(SubCategoryService SubCategorieservice)
        {
            _SubCategoriesService = SubCategorieservice;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get()
        {
            try
            {
                return Ok(_SubCategoriesService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid subcategoryId)
        {
            try
            {
                return Ok(_SubCategoriesService.GetById(subcategoryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetSubCategoriesByCategory")]
        public ActionResult GetSubCategoriesByCategory(Guid categoryId)
        {
            try
            {
                return Ok(_SubCategoriesService.GetSubCategoriesByCategory(categoryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(SubCategory subCategory)
        {
            try
            {
                return Ok(_SubCategoriesService.Add(subCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(SubCategory category)
        {
            try
            {
                var SubCategory = _SubCategoriesService.Update(category);
                return Ok(SubCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Delete")]
        public ActionResult Delete(Guid subCategoryId)
        {
            try
            {
                return Ok(_SubCategoriesService.Delete(subCategoryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
