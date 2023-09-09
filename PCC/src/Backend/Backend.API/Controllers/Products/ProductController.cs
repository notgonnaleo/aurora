using Backend.API.Helpers.Controllers.Extensions;
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
    public class ProductController : CustomController
    {
        private readonly ProductService _productService;
        private readonly UserContextService _userContextService;

        public ProductController(ProductService productService, UserContextService userContextService)
            : base(userContextService)
        {
            _productService = productService;
            _userContextService = userContextService;
        }

        [ValidateUserContext]
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _productService.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            try
            {
                return Ok(await _productService.GetById(Id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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

                throw ex;
            }
        }

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