using Backend.Domain.Entities.OrderHistories.Request;
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
        [HttpGet, Route("GetOrders")]
        public ActionResult GetOrders(Guid tenantId) // in the future filter by date or even client bcuz this is going to be huge 
        {
            try
            {
                return Ok(_orderService.GetOrders(tenantId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetOrder")]
        public ActionResult GetOrder(Guid tenantId, Guid orderId, string? orderCode)
        {
            try
            {
                return Ok(_orderService.GetOrder(tenantId, orderId, orderCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetOrderEntities")]
        public ActionResult GetOrderEntities(Guid tenantId, Guid orderId, string? orderCode)
        {
            try
            {
                return Ok(_orderService.GetOrder(tenantId, orderId, orderCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost, Route("Approve")]
        public ActionResult ApproveOrder(Guid tenantId, Guid orderId)
        {
            try
            {
                return Ok(_orderService.ApproveOrder(tenantId, orderId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost, Route("Cancel")]
        public ActionResult CancelOrder(Guid tenantId, Guid orderId)
        {
            try
            {
                return Ok(_orderService.CancelOrder(tenantId, orderId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPut, Route("UpdateOrderItem")]
        public ActionResult UpdateOrderItem(OrderItemsRequest orderRequest)
        {
            try
            {
                return Ok(_orderService.UpdateOrderItem(orderRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpDelete, Route("RemoveOrderItem")]
        public ActionResult RemoveOrderItem(OrderItemsRequest orderRequest)
        {
            try
            {
                return Ok(_orderService.RemoveOrderItem(orderRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpGet, Route("GetOrderEntryHistoryLog")]
        public ActionResult GetOrderEntryHistoryLog(Guid tenantId, Guid orderId)
        {
            try
            {
                return Ok(_orderService.GetOrderEntryHistoryLog(tenantId, orderId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [TypeFilter(typeof(ValidateUserContextAttribute))]
        [HttpPost, Route("ExecuteOrderMovementAction")]
        public ActionResult ExecuteOrderMovementAction(OrderMovementEntryHistoryRequest action)
        {
            try
            {
                return Ok(_orderService.ExecuteOrderMovementAction(action));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
