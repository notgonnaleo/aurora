using Backend.Domain.Entities.Addresses;
using Frontend.Web.Models.Route;
using Frontend.Web.Repository.Client;
using Backend.Infrastructure.Enums.Modules;
using System.Net.Http.Json;
using Frontend.Web.Models.Client;
using Backend.Domain.Entities.Orders.Response;
using Backend.Domain.Entities.Orders.Request;

namespace Frontend.Web.Repository.Orders
{
    public class OrderRepository
    {
        private readonly HttpClientRepository _httpClientRepository;

        public OrderRepository(HttpClientRepository httpClientRepository)
        {
            _httpClientRepository = httpClientRepository;
        }

        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrders(string tenantId)
        {
            var parameters = new RouteParameterRequest() { ParameterName = Methods.Orders.GET.GetOrders.Args.tenantId, ParameterValue = tenantId };
            var request = new RouteBuilder<OrderResponse>().Send(Endpoints.Orders, Methods.Orders.GET.GetOrder.GetOrderEndpointName, parameters);
            return await _httpClientRepository.Get(request);
        }

        public async Task<ApiResponse<OrderResponse>> GetOrder(string tenantId, string orderId, string? orderCode)
        {
            var parameters = new List<RouteParameterRequest>()
            {
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Orders.GET.GetOrder.Args.tenantId,
                    ParameterValue = tenantId,
                },
                new RouteParameterRequest()
                {
                    ParameterName = Methods.Orders.GET.GetOrder.Args.orderId,
                    ParameterValue = orderId,
                }
            };
            if(orderCode is not null)
            {
                parameters.Add(new RouteParameterRequest() 
                { 
                    ParameterName = Methods.Orders.GET.GetOrder.Args.orderCode, 
                    ParameterValue = orderCode 
                });
            }
            var request = new RouteBuilder<OrderResponse>().SendMultiple(Endpoints.Orders, Methods.Orders.GET.GetOrder.GetOrderEndpointName, parameters);
            return await _httpClientRepository.Find(request);
        }

        public async Task<ApiResponse<OrderOpeningConfirmation>> OpenNewOrder(OrderRequest orderRequest)
        {
            var model = new RouteBuilder<OrderRequest>().Send(Endpoints.Orders, Methods.Orders.POST.OpenNewOrder.OpenNewOrderEndpointName, orderRequest);
            return await _httpClientRepository.Post<OrderRequest, OrderOpeningConfirmation>(model);
        }

        public async Task<ApiResponse<Address>> UpdateAddress(Address address)
        {
            var model = new RouteBuilder<Address>().Send(Endpoints.Addresses, Methods.Default.PUT, address);
            return await _httpClientRepository.Put(model);
        }

        public async Task<bool> DeleteAddress(string tenantId, string addressId)
        {
            var parameters = new List<RouteParameterRequest>()
        {
            new RouteParameterRequest()
            {
                ParameterName = Methods.Addresses.DELETE.DeleteAddress.tenantId,
                ParameterValue = tenantId,
            },
            new RouteParameterRequest()
            {
                ParameterName = Methods.Addresses.DELETE.DeleteAddress.addressId,
                ParameterValue = addressId,
            }
        };
            var request = new RouteBuilder<Address>().SendMultiple(Endpoints.Addresses, Methods.Default.DELETE, parameters);
            return (await _httpClientRepository.Put(request)).Success;
        }
    }
}
