using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.ProductTypes;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.ProductTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.ProductTypes
{
    [ApiController]
    [Route("ProductTypes")]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService _productTypeService;
        private readonly UserContextService _userContextService;

        public ProductTypeController(ProductTypeService productTypeService, UserContextService userContextService)
        {
            _productTypeService = productTypeService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _productTypeService.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _productTypeService.GetById(Id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(ProductType productType)
        {
            try
            {
                return Ok(await _productTypeService.Add(productType));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
