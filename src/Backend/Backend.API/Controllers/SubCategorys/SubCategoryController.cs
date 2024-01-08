using Backend.Domain.Entities.Category;
using Backend.Domain.Entities.SubCategory;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.SubCategories
{
    [ApiController]
    [Route("SubCategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategorieservice _SubCategorieservice;

        public SubCategoryController(SubCategorieservice SubCategorieservice)
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
                return Ok(await _SubCategorieservice.Get(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid subcategoryId,Guid tenantId)
        {
            try
            {
                return Ok(await _SubCategorieservice.GetById(subcategoryId,tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(SubCategory subCategory, Guid categoryId)
        {
            try
            {
                return Ok(await _SubCategorieservice.Add(subCategory, categoryId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(SubCategory category, Guid SubCategoryId)
        {
            try
            {
                var SubCategory = await _SubCategorieservice.Update(category, SubCategoryId);

                if (SubCategory == null)
                    return NotFound($"Produto com ID {SubCategoryId} não encontrado.");

                return Ok(SubCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
