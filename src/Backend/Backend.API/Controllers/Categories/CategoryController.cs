using Backend.Domain.Entities.Categorys;
using Backend.Domain.Entities.ProductTypes;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Categorys;
using Backend.Infrastructure.Services.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.API.Controllers.Categorys
{
    [ApiController]
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly UserContextService _userContextService;

        public CategoryController(CategoryService categoryService, UserContextService userContextService)
        {
            _categoryService = categoryService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get(Guid tenantId)
        {
            try
            {
                return Ok(await _categoryService.Get(tenantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid categoryId, Guid tenantId)
        {
            try
            {
                return Ok(await _categoryService.GetById(categoryId,tenantId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Category category)
        {
            try
            {
                return Ok(await _categoryService.Add(category));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(Category category, Guid categoryId)
        {
            try
            {
                var Category = await _categoryService.Update(category, categoryId);

                if (Category == null)
                {
                    return NotFound("The category searched does not exist or is invalid");
                }

                return Ok(Category);
            }
            catch (Exception ex)
            {
                // Registre o erro em um log ou trate de acordo com seus requisitos
                return StatusCode(500,ex.Message);
            }
        }


    }
}
