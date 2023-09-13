using Backend.API.Helpers.Controllers.Extensions;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("ProductTypes")]
    public class ProductTypeController : CustomController
    {
        private readonly ProductTypeService _productTypeService;
        private readonly UserContextService _userContextService;

        public ProductTypeController(ProductTypeService productTypeService, UserContextService userContextService)
        : base(userContextService)
        {
            _productTypeService = productTypeService;
            _userContextService = userContextService;
        }

        [ValidateUserContext]
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

        [ValidateUserContext]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid Id)
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

        [ValidateUserContext]
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

        [ValidateUserContext]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(ProductType productType)
        {
            try
            {
                return Ok(await _productTypeService.Update(productType));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [ValidateUserContext]
        [HttpPut]
        [Route("Delete")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                return Ok(await _productTypeService.Delete(Id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}