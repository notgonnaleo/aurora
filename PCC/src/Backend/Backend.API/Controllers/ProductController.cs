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
    }
}