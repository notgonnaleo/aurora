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
        private readonly UserContextService _userContextService;

        public ProductController(ProductService productService, UserContextService userContextService)
        {
            _productService = productService;
            _userContextService = userContextService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get(Guid tenantId)
        {
            try
            {
                return Ok(await _productService.Get(tenantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid tenantId, Guid productId)
        {
            try
            {
                return Ok(await _productService.GetById(tenantId, productId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Product product)
        {
            try
            {
                var context = _userContextService.LoadContext();
                product.CreatedBy = context.UserId;

                return Ok(await _productService.Add(product));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(Product product)
        {
            try
            {
                return Ok(await _productService.Update(product));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Delete")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                return Ok(await _productService.Delete(Id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}