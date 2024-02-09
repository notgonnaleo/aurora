using Backend.Domain.Entities.Categories;
using Backend.Domain.Entities.ProductTypes;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Categories;
using Backend.Infrastructure.Services.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.API.Controllers.Categories
{
    [ApiController]
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _Categorieservice;
        private readonly UserContextService _userContextService;

        public CategoryController(CategoryService Categorieservice, UserContextService userContextService)
        {
            _Categorieservice = Categorieservice;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get()
        {
            try
            {
                return Ok(_Categorieservice.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid categoryId)
        {
            try
            {
                return Ok(_Categorieservice.GetById(categoryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(Category category)
        {
            try
            {
                return Ok(_Categorieservice.Add(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Category category)
        {
            try
            {
                return Ok(_Categorieservice.Update(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetCategoryAndSubCategories")]
        public ActionResult GetCategoryAndSubCategories()
        {
            try
            {
                return Ok(_Categorieservice.GetCategoryAndSubCategories());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Delete")]
        public ActionResult Delete(Guid categoryId)
        {
            try
            {
                return Ok(_Categorieservice.Delete(categoryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
