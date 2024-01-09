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
        public ActionResult Get(Guid tenantId)
        {
            try
            {
                return Ok(_productService.Get(tenantId));
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(Product product)
        {
            try
            {
                return Ok(_productService.Add(product));
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid Id)
        {
            try
            {
                return Ok(_productService.Delete(Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}