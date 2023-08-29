using Backend.Domain.Entities.Products;
using Backend.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Product> products = await _productService.Get();
            return Ok(products);
        }

        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            Product product = await _productService.GetById(Id);
            return Ok(product);
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