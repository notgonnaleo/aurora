using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.Stocks;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Stocks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Stocks
{
    [ApiController]
    [Route("Stock")]
    public class StockController : ControllerBase
    {
        private readonly UserContextService _userContextService;
        private readonly StockService _stockService;

        public StockController(UserContextService userContextService, StockService stockService)
        {
            _userContextService = userContextService;
            _stockService = stockService;
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(Stock stock)
        {
            try
            {
                return Ok(_stockService.Add(stock));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("List")]
        public ActionResult Get()
        {
            try
            {
                return Ok(_stockService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetProductInventory")]
        public ActionResult GetInventory(Guid productId, Guid? variantId)
        {
            try
            {
                return Ok(_stockService.GetProductInventory(productId, variantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("GetStockWithDetail")]
        public ActionResult GetStockWithDetail(Guid tenantId)
        {
            try
            {
                return Ok(_stockService.GetStockWithDetail(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet]
        [Route("Find")]
        public ActionResult GetById(Guid tenantId, Guid stockMovementId)
        {
            try
            {
                return Ok(_stockService.GetById(tenantId, stockMovementId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Domain.Entities.Stocks.Stock model)
        {
            try
            {

                return Ok(_stockService.Update(model));



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Guid stockMovementId)
        {
            try
            {
                return Ok(_stockService.Delete(stockMovementId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
