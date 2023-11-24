using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
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

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(ProductType productType, int id)
        {
            try
            {
                var updatedProductType = await _productTypeService.Update(productType, id);

                if (updatedProductType == null)
                {
                    return NotFound($"Produto com ID {id} não encontrado.");
                }

                return Ok(updatedProductType);
            }
            catch (Exception ex)
            {
                // Registre o erro em um log ou trate de acordo com seus requisitos
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
