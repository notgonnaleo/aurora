using Backend.Domain.Entities.Orders.Request;
using Backend.Domain.Entities.Orders.Response;
using Frontend.Web.Models.Client;
using Frontend.Web.Repository.Orders;

namespace Frontend.Web.Services.Orders
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrders(string tenantId)
        {
            return await _orderRepository.GetOrders(tenantId);
        }
        public async Task<ApiResponse<OrderResponse>> GetOrder(string tenantId, string orderId, string? orderCode )
        {
            return await _orderRepository.GetOrder(tenantId, orderId, orderCode);
        }
        public async Task<ApiResponse<OrderOpeningConfirmation>> OpenNewOrder(OrderRequest orderRequest)
        {
            return await _orderRepository.OpenNewOrder(orderRequest);
        }
    }
}
