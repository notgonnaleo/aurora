using Backend.Domain.Entities.OrderItems;
using Backend.Domain.Entities.OrderItems.Request;
using Backend.Domain.Entities.OrderItems.Response;
using Backend.Domain.Entities.Orders;
using Backend.Domain.Entities.Orders.Request;
using Backend.Domain.Entities.Orders.Response;
using Backend.Domain.Entities.Products;
using Backend.Domain.Enums.Orders;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Migrations.AppDbMigration;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Orders
{
    public class OrderService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly ProductService _productService;
        private readonly ProductVariantService _productVariantService;
        private readonly AgentService _agentService;
        public OrderService(UserContextService userContextService, AppDbContext appDbContext, ProductService productService, ProductVariantService productVariantService, AgentService agentService) : base(userContextService)
        {
            _appDbContext = appDbContext;
            _productService = productService;
            _productVariantService = productVariantService;
            _agentService = agentService;
        }

        public OrderResponse GetOrder(Guid tenantId, Guid orderId, string? orderCode)
        {
            var orders = _appDbContext.Orders.Where(x => x.TenantId == tenantId && x.OrderId == orderId).FirstOrDefault();
            if (orders is null) return new OrderResponse();

            var orderItems = _appDbContext.OrderItems.Where(x => x.TenantId == tenantId && x.OrderId == orderId).ToList();
            IEnumerable<OrderItemsResponse> orderItemsResponse = new List<OrderItemsResponse>();
            if (orderItems.Any())
            {
                orderItemsResponse = orderItems.Select(orderItem => new OrderItemsResponse()
                {
                    TenantId = orderItem.TenantId,
                    OrderItemId = orderItem.OrderItemId,
                    Item = _productService.GetProductThumbnail(tenantId, orderItem.ProductId),
                    ItemVariant = null,
                    ItemQuantity = orderItem.ItemQuantity,
                    ItemTotalAmount = orderItem.ItemTotalAmount,
                    ItemUnitAmount = orderItem.ItemUnitAmount,
                });
            }
            return new OrderResponse()
            {
                TenantId = orders.TenantId,
                OrderId = orders.OrderId,
                OrderEffectiveDate = orders.OrderEffectiveDate.GetValueOrDefault(),
                OrderEstimatedDate = orders.OrderEstimatedDate,
                OrderOpeningDate = orders.OrderOpeningDate,
                OrderCode = orders.OrderCode,
                OrderItems = orderItemsResponse,
                OrderStatus = new OrderStatus() { OrderStatusId = orders.OrderStatusId, OrderStatusName = ((OrdersStatusEnums)orders.OrderStatusId).ToString(), },
            };
        }

        public OrderOpeningConfirmation OpenNewOrder(OrderRequest orderRequest)
        {
            var newOrder = new Order(orderRequest);
            newOrder.CreatedBy = LoadContext().UserId;

            var customer = _agentService.GetCustomer(newOrder.TenantId, newOrder.CustomerId);
            var seller = _agentService.GetSeller(newOrder.TenantId, newOrder.SellerId);

            // Abstract this
            if (customer is null)
                throw new Exception("Customer must be selected");
            if (seller is null)
                throw new Exception("Seller must be selected");

            if (AddOrder(newOrder)) 
            {
                if (orderRequest.OrderItems is not null && orderRequest.OrderItems.Any())
                {
                    BulkInsertItemsIntoOrder(orderRequest.OrderItems, newOrder.OrderId);
                }

                return new OrderOpeningConfirmation()
                {
                    TenantId = newOrder.TenantId,
                    OrderId = newOrder.OrderId,
                    OrderCode = newOrder.OrderCode,
                    OrderEstimatedDate = newOrder.OrderEstimatedDate,
                    OrderOpeningDate = newOrder.OrderOpeningDate,
                    OrderStatus = new OrderStatus() { OrderStatusId = newOrder.OrderStatusId, OrderStatusName = ((OrdersStatusEnums)newOrder.OrderStatusId).ToString(), },
                    Customer = new Domain.Entities.Agents.Response.CustomerThumbnail()
                    {
                        AgentId = customer.AgentId,
                        AgentDisplayName = customer.Name
                    },
                    Seller = new Domain.Entities.Agents.Response.SellerThumbnail()
                    {
                        AgentId = seller.AgentId,
                        AgentDisplayName = seller.Name
                    },
                };
            };
            throw new Exception("Could not create a new order request.");
        }

        public bool BulkInsertItemsIntoOrder(IEnumerable<OrderItemsRequest> orderItems, Guid orderId)
        {
            // Someone with more expertise knows that this sucks.
            // better approach is bulking insert through SQL
            var orderLines = new List<OrderItem>();
            foreach (var item in orderItems)
            {
                if (item.ItemQuantity == 0)
                    throw new Exception("By selecting an item you must provide the desired quantity");

                item.OrderId = orderId;
                var product = _productService.GetProductThumbnail(item.TenantId, item.ItemId);
                OrderItem orderLine = new OrderItem();
                ProductVariant? variant = null;
                if (item.ItemVariantId is not null)
                {
                    variant = _productVariantService.GetVariant(item.TenantId, item.ItemId, item.ItemVariantId.Value);
                }
                if(variant is not null)
                {
                    orderLine = new OrderItem(item, Convert.ToDecimal(product.TotalWeight), Convert.ToDecimal(product.Value), item.ItemQuantity);
                    orderLines.Add(orderLine);
                    AddOrderItem(orderLine);
                    continue;
                }
                orderLine = new OrderItem(item, Convert.ToDecimal(product.TotalWeight), Convert.ToDecimal(product.Value), item.ItemQuantity);
                orderLines.Add(orderLine);
                AddOrderItem(orderLine);
            }
            return orderLines.Any();
        }

        public bool AddOrderItem(OrderItem orderItem)
        {
            _appDbContext.OrderItems.Add(orderItem);
            return _appDbContext.SaveChanges() > 0;
        }

        public bool AddOrder(Order order)
        {
            _appDbContext.Orders.Add(order);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
