using Backend.Domain.Entities.Categorys;
using Backend.Domain.Entities.SubCategory;
using Backend.Infrastructure.Services.Categorys;
using Backend.Infrastructure.Services.SubCategorys;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.SubCategorys
{
    [ApiController]
    [Route("SubCategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryService _subCategoryService;

        public SubCategoryController(SubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get(Guid tenantId)
        {
            try
            {
                return Ok(await _subCategoryService.Get(tenantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid subcategoryId,Guid tenantId)
        {
            try
            {
                return Ok(await _subCategoryService.GetById(subcategoryId,tenantId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(SubCategory subCategory, string categoryId)
        {
            try
            {
                return Ok(await _subCategoryService.Add(subCategory, categoryId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(SubCategory category, Guid SubCategoryId)
        {
            try
            {
                var SubCategory = await _subCategoryService.Update(category, SubCategoryId);

                if (SubCategory == null)
                {
                    return NotFound($"Produto com ID {SubCategoryId} não encontrado.");
                }

                return Ok(SubCategory);
            }
            catch (Exception ex)
            {
                // Registre o erro em um log ou trate de acordo com seus requisitos
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
