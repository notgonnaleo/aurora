using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Products
{
    [ApiController]
    [Route("ProductMedia")]
    public class ProductMediaController : ControllerBase
    {
        private readonly ProductMediaService _productMediaService;
        private readonly UserContextService _userContextService;

        public ProductMediaController(ProductMediaService productMediaService, UserContextService userContextService)
        {
            _productMediaService = productMediaService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get(Guid tenantId, Guid productMediaId)
        {
            try
            {
                return Ok(_productMediaService.Get(tenantId, productMediaId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid tenantId, Guid productId, Guid id)
        {
            try
            {
                return Ok(_productMediaService.GetById(tenantId, productId, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(ProductMedia productMedia)
        {
            try
            {
                return Ok(await _productMediaService.Add(productMedia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(ProductMedia productMedia)
        {
            try
            {
                return Ok(_productMediaService.Update(productMedia));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid tenantId, Guid productId, Guid Id)
        {
            try
            {
                return Ok(_productMediaService.Delete(tenantId, productId, Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}