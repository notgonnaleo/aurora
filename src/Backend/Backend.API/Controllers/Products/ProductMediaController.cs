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

        public ProductMediaController(ProductMediaService productMediaService)
        {
            _productMediaService = productMediaService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get(Guid productId)
        {
            try
            {
                return Ok(_productMediaService.Get(productId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_productMediaService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(ProductMedia model)
        {
            try
            {
                return Ok(await _productMediaService.Add(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> UploadFile([FromBody] string base64string)
        {
            try
            {
                return Ok(await _productMediaService.UploadFile(base64string));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(ProductMedia model)
        {
            try
            {
                return Ok(_productMediaService.Update(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_productMediaService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}