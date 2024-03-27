using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Products
{
    [ApiController]
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get(Guid tenantId)
        {
            try
            {
                return Ok(_productService.GetProductsWithDetail(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid tenantId, Guid productId)
        {
            try
            {
                return Ok(_productService.GetById(tenantId, productId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Product product)
        {
            try
            {
                return Ok(await _productService.Add(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Product product)
        {
            try
            {
                return Ok(_productService.Update(product));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Delete")]
        public ActionResult Delete(Guid tenantId, Guid ProductId)
        {
            try
            {
                return Ok(_productService.Delete(tenantId, ProductId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}