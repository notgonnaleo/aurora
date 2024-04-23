using Backend.Domain.Entities.OrderItems.Request;
using Backend.Domain.Entities.Orders.Request;
using Backend.Domain.Entities.Orders.Response;
using Backend.Infrastructure.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Orders
{
    [ApiController]
    [Route("Orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService) 
        {
            _orderService = orderService;
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetOrder")]
        public ActionResult GetOrder(Guid tenantId, Guid orderId, string? orderCode)
        {
            return Ok(_orderService.GetOrder(tenantId, orderId, orderCode));
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost, Route("OpenNewOrder")]
        public ActionResult OpenNewOrder(OrderRequest orderRequest)
        {
            try
            {
                return Ok(_orderService.OpenNewOrder(orderRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
