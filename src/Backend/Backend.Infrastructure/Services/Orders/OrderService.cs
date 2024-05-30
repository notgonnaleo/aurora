using Backend.Domain.Entities.OrderHistories;
using Backend.Domain.Entities.OrderHistories.Request;
using Backend.Domain.Entities.OrderHistories.Response;
using Backend.Domain.Entities.OrderItems;
using Backend.Domain.Entities.OrderItems.Request;
using Backend.Domain.Entities.OrderItems.Response;
using Backend.Domain.Entities.Orders;
using Backend.Domain.Entities.Orders.Request;
using Backend.Domain.Entities.Orders.Response;
using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.Stocks;
using Backend.Domain.Enums.Orders;
using Backend.Domain.Enums.StockMovements.MovementType;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Migrations.AppDbMigration;
using Backend.Infrastructure.Services.Agents;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using Backend.Infrastructure.Services.Products;
using Backend.Infrastructure.Services.Stocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Infrastructure.Enums.Modules.Methods;
using static System.Collections.Specialized.BitVector32;
using MovementTypes = Backend.Domain.Enums.StockMovements.MovementType.MovementTypes;
using Stock = Backend.Domain.Entities.Stocks.Stock;

namespace Backend.Infrastructure.Services.Orders
{
    public class OrderService : Service
    {
        private readonly AppDbContext _appDbContext;
        private readonly ProductService _productService;
        private readonly ProductVariantService _productVariantService;
        private readonly AgentService _agentService;
        private readonly StockService _stockService;

        public OrderService(UserContextService userContextService, AppDbContext appDbContext, ProductService productService, ProductVariantService productVariantService, AgentService agentService, StockService stockService) : base(userContextService)
        {
            _appDbContext = appDbContext;
            _productService = productService;
            _productVariantService = productVariantService;
            _agentService = agentService;
            _stockService = stockService;
        }

        public IEnumerable<OrderResponse> GetOrders(Guid tenantId)
        {
            // this sucks part 2
            var orders = _appDbContext.Orders.Where(x => x.TenantId == tenantId && x.Active).ToList();
            if (orders is null) return new List<OrderResponse>();
            var ordersThumbnails = new List<OrderResponse>();

            foreach (var order in orders)
            {
                var orderItems = _appDbContext.OrderItems.Where(x => x.TenantId == tenantId && x.OrderId == order.OrderId).ToList();
                IEnumerable<OrderItemsResponse> orderItemsResponse = new List<OrderItemsResponse>();
                if (orderItems.Any())
                {
                    orderItemsResponse = orderItems.Select(orderItem => new OrderItemsResponse()
                    {
                        TenantId = orderItem.TenantId,
                        OrderId = orderItem.OrderId,
                        OrderItemId = orderItem.OrderItemId,
                        Item = _productService.GetProductThumbnail(tenantId, orderItem.ProductId),
                        ItemVariant = null,
                        ItemQuantity = orderItem.ItemQuantity,
                        ItemTotalAmount = orderItem.ItemTotalAmount,
                        ItemUnitAmount = orderItem.ItemUnitAmount,
                    });
                }
                ordersThumbnails.Add(new OrderResponse()
                {
                    TenantId = order.TenantId,
                    OrderId = order.OrderId,
                    OrderEffectiveDate = order.OrderEffectiveDate.GetValueOrDefault(),
                    OrderEstimatedDate = order.OrderEstimatedDate,
                    OrderOpeningDate = order.OrderOpeningDate,
                    OrderCode = order.OrderCode,
                    OrderItems = orderItemsResponse,
                    OrderParcelAmount = order.ParcelsQuantity,
                    OrderTotalAmount = order.OrderTotalAmount,
                    OrderStatus = new OrderStatus() { OrderStatusId = order.OrderStatusId, OrderStatusName = ((OrdersStatusEnums)order.OrderStatusId).ToString(), },
                });
            }
            return ordersThumbnails;
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
                Customer = _agentService.GetAgentDetails(orders.CustomerId),
                Seller = _agentService.GetAgentDetails(orders.SellerId),
                OrderItems = orderItemsResponse,
                OrderStatus = new OrderStatus() { OrderStatusId = orders.OrderStatusId, OrderStatusName = ((OrdersStatusEnums)orders.OrderStatusId).ToString(), },
            };
        }

        public OrderOpeningConfirmation OpenNewOrder(OrderRequest orderRequest)
        {
            var newOrder = new Order(orderRequest);
            newOrder.CreatedBy = LoadContext().UserId;

            var customer = _agentService.GetCustomer(newOrder.TenantId, newOrder.CustomerId);
            var seller = _agentService.GetEmployee(newOrder.TenantId, newOrder.SellerId);

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
                    Seller = new Domain.Entities.Agents.Response.EmployeeThumbnail()
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
                var stockResult = new Inventory();
                if (item.ItemVariantId is not null)
                {
                    stockResult = _stockService.GetProductStock(item.TenantId, item.ItemId, item.ItemVariantId);
                    if (stockResult.TotalAmount == 0 || stockResult.TotalAmount < item.ItemQuantity)
                        throw new Exception($"You need more of the selected item to make a movement");
                }
                stockResult = _stockService.GetProductStock(item.TenantId, item.ItemId, null);
                if (stockResult.TotalAmount == 0 || stockResult.TotalAmount < item.ItemQuantity)
                    throw new Exception($"You need more of the selected item to make a movement");

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
                if (variant is not null)
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
        public bool UpdateOrderItem(OrderItemsRequest orderItemRequest)
        {
            var orderItem = new OrderItem(orderItemRequest);
            _appDbContext.OrderItems.Update(orderItem);
            return _appDbContext.SaveChanges() > 0;
        }
        public bool RemoveOrderItem(OrderItemsRequest orderItemRequest)
        {
            var orderItem = new OrderItem(orderItemRequest);
            _appDbContext.OrderItems.Remove(orderItem);
            return _appDbContext.SaveChanges() > 0;
        }
        public bool AddOrder(Order order)
        {
            _appDbContext.Orders.Add(order);
            return _appDbContext.SaveChanges() > 0;
        }

        public IEnumerable<OrderMovementEntryHistoryResponse> GetOrderEntryHistoryLog(Guid tenantId, Guid orderId)
        {
            var history = _appDbContext.OrderHistories.Where(x => x.TenantId == tenantId && x.OrderId == orderId).ToList();
            var items = _appDbContext.OrderItems.Where(x => x.TenantId == tenantId && x.OrderId == orderId).ToList();
            var entries = new List<OrderMovementEntryHistoryResponse>();
            foreach (var historyItem in history)
            {
                var orderItem = _appDbContext.OrderItems.FirstOrDefault(x => x.OrderItemId == historyItem.OrderItemId);
                if (orderItem is null)
                {
                    continue;
                }

                entries.Add(new OrderMovementEntryHistoryResponse()
                {
                    OrderId = historyItem.OrderId,
                    OrderHistoryId = historyItem.OrderHistoryId,
                    OrderTotalItemsMovement = historyItem.OrderTotalItemsMovement,
                    OrderMovementType = historyItem.OrderMovementType,
                    From = _agentService.GetAgentDetails(historyItem.From),
                    To = _agentService.GetAgentDetails(historyItem.To),
                    MovementTimestamp = historyItem.Created.Value,
                    Item = new ItemThumbnail()
                    {
                        ProductId = orderItem.ProductId,
                        VariantId = orderItem.VariantId,
                        ItemName = orderItem.VariantId.HasValue && orderItem.VariantId.Value != Guid.Empty
                        ? _productVariantService.GetVariant(orderItem.TenantId, orderItem.ProductId, orderItem.VariantId.Value).Name 
                        : _productService.GetById(orderItem.TenantId, orderItem.ProductId).Name,
                        OrderItemId = orderItem.OrderItemId,
                        ItemValue = orderItem.ItemUnitAmount,
                        Quantity = orderItem.ItemQuantity,
                        Value = orderItem.ItemTotalAmount
                    }

                });
            }
            return entries;
        }

        public bool ExecuteOrderMovementAction(OrderMovementEntryHistoryRequest action)
        {
            if (action.OrderTotalItemsMovement == 0)
                throw new Exception("You cannot send an empty movement");

            var context = LoadContext();
            // Get the order and items
            var order = _appDbContext.Orders.Include(_ => _.OrderItems).First(_ => _.OrderId == action.OrderId);

            // Insert into order history
            var orderHistory = new OrderHistory()
            {
                TenantId = context.Tenant.Id,
                OrderHistoryId = action.OrderHistoryId,
                OrderId = action.OrderId,
                OrderItemId = action.OrderItemId,
                OrderMovementType = (int)MovementTypes.Input,
                OrderTotalItemsMovement = action.OrderTotalItemsMovement,
                From = action.From,
                To = action.To,
                Active = true,
                Created = DateTime.UtcNow,
                CreatedBy = context.UserId,
                Updated = null,
                UpdatedBy = null,
            };
            _appDbContext.OrderHistories.Add(orderHistory);
            _appDbContext.SaveChanges();

            // Calculate total amount already sent based on the order items history log
            Dictionary<Guid, int> productQuantity = new Dictionary<Guid, int>();
            var history = _appDbContext.OrderHistories.Where(x => x.OrderId == action.OrderId).ToList();
            foreach (var h in history)
            {
                if (productQuantity.ContainsKey(h.OrderItemId))
                {
                    var quantity = productQuantity[h.OrderItemId];
                    quantity += h.OrderTotalItemsMovement;
                    productQuantity[h.OrderItemId] = quantity;
                }
                else
                {
                    var quantity = 0;
                    quantity += h.OrderTotalItemsMovement;
                    productQuantity.Add(h.OrderItemId, quantity);
                }
            }

            // Compare the total amount with expected amount and update the status
            bool isDone = true;
            var items = order.OrderItems;
            foreach (var item in items)
            {
                if (productQuantity.ContainsKey(item.OrderItemId))
                {
                    var quantity = productQuantity[item.OrderItemId];
                    if (item.ItemQuantity > quantity)
                    {
                        isDone = false;
                    }

                    _appDbContext.Stocks.Add(new Domain.Entities.Stocks.Stock()
                    {
                        TenantId = context.Tenant.Id,
                        UserId = context.UserId,
                        AgentId = order.SellerId, // always
                        ProductId = item.ProductId,
                        VariantId = item.VariantId.HasValue ? item.VariantId.Value : null,
                        MovementType = (int)MovementTypes.Output,
                        Quantity = quantity,
                        MovementDate = DateTime.Now,
                        CreatedBy = context.UserId,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        UpdatedBy = null,
                        Active = true,
                    });
                    _appDbContext.SaveChanges();
                }
                else
                {
                    isDone = false;
                }
            }

            // update status on order
            if (isDone)
                order.OrderStatusId = (int)OrdersStatusEnums.Done;
            else
                order.OrderStatusId = (int)OrdersStatusEnums.PartiallyDone;
            _appDbContext.Update(order);
            _appDbContext.SaveChanges();
            return true;
        }

        public bool RefundOrder(Guid tenantId, Guid orderId)
        {
            var context = LoadContext();
            var order = _appDbContext.Orders
                .Include(_ => _.OrderItems)
                .First(_ => _.OrderId == orderId);
            foreach (var itemGroup in order.OrderItems.GroupBy(x => x.OrderItemId))
            {
                // Assuming you want to create an order history entry for each item in the group
                foreach (var item in itemGroup)
                {
                    // Check if stock is finished
                    Dictionary<Guid, int> productQuantity = new Dictionary<Guid, int>();
                    var history = _appDbContext.OrderHistories.Where(x => x.OrderId == item.OrderId);
                    var totalAlreadySent = history.Where(x => x.OrderId == orderId &&
                    x.OrderItemId == item.OrderItemId)
                        .Sum(x => x.OrderTotalItemsMovement);

                    var orderHistory = new OrderHistory()
                    {
                        TenantId = tenantId,
                        OrderHistoryId = Guid.NewGuid(),
                        OrderId = orderId,
                        OrderItemId = item.OrderItemId,
                        OrderMovementType = 0,
                        OrderTotalItemsMovement = totalAlreadySent,
                        From = order.CustomerId,
                        To = order.SellerId,
                        Active = true,
                        Created = DateTime.UtcNow,
                        CreatedBy = context.UserId,
                        Updated = null,
                        UpdatedBy = null,
                    };
                    _appDbContext.OrderHistories.Add(orderHistory);

                    _appDbContext.Stocks.Add(new Domain.Entities.Stocks.Stock()
                    {
                        TenantId = context.Tenant.Id,
                        UserId = context.UserId,
                        AgentId = order.SellerId, // always
                        ProductId = item.ProductId,
                        VariantId = item.VariantId.HasValue ? item.VariantId.Value : null,
                        MovementType = 1,
                        Quantity = totalAlreadySent,
                        MovementDate = DateTime.Now,
                        CreatedBy = context.UserId,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        UpdatedBy = null,
                        Active = true,
                    });
                }
            }
            return UpdateOrderStatus(tenantId, orderId, (int)OrdersStatusEnums.Refunding);
        }
        public bool CancelOrder(Guid tenantId, Guid orderId)
        {
            var context = LoadContext();
            var order = _appDbContext.Orders
                .Include(_ => _.OrderItems)
                .First(_ => _.OrderId == orderId);
            foreach (var itemGroup in order.OrderItems.GroupBy(x => x.OrderItemId))
            {
                // Assuming you want to create an order history entry for each item in the group
                foreach (var item in itemGroup)
                {
                    // Check if stock is finished
                    Dictionary<Guid, int> productQuantity = new Dictionary<Guid, int>();
                    var history = _appDbContext.OrderHistories.Where(x => x.OrderId == item.OrderId);
                    var totalAlreadySent = history.Where(x => x.OrderId == orderId &&
                    x.OrderItemId == item.OrderItemId)
                        .Sum(x => x.OrderTotalItemsMovement);

                    var orderHistory = new OrderHistory()
                    {
                        TenantId = tenantId,
                        OrderHistoryId = Guid.NewGuid(),
                        OrderId = orderId,
                        OrderItemId = item.OrderItemId,
                        OrderMovementType = 0,
                        OrderTotalItemsMovement = totalAlreadySent,
                        From = order.CustomerId,
                        To = order.SellerId,
                        Active = true,
                        Created = DateTime.UtcNow,
                        CreatedBy = context.UserId,
                        Updated = null,
                        UpdatedBy = null,
                    };
                    _appDbContext.OrderHistories.Add(orderHistory);
                    _appDbContext.Stocks.Add(new Domain.Entities.Stocks.Stock()
                    {
                        TenantId = context.Tenant.Id,
                        UserId = context.UserId,
                        AgentId = order.SellerId, // always
                        ProductId = item.ProductId,
                        VariantId = item.VariantId.HasValue ? item.VariantId.Value : null,
                        MovementType = 1, 
                        Quantity = totalAlreadySent,
                        MovementDate = DateTime.Now,
                        CreatedBy = context.UserId,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        UpdatedBy = null,
                        Active = true,
                    });
                }
            }
            return UpdateOrderStatus(tenantId, orderId, (int)OrdersStatusEnums.Canceled);
        }
        public bool ApproveOrder(Guid tenantId, Guid orderId)
        {
            return UpdateOrderStatus(tenantId, orderId, (int)OrdersStatusEnums.InProgress);
        }

        public bool UpdateOrderStatus(Guid tenantId, Guid orderId, int statusId)
        {
            var context = LoadContext();
            var order = _appDbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
            if (order is null) throw new Exception("Order does not exist and cannot be approved.");
            order.OrderStatusId = statusId;
            _appDbContext.Orders.Update(order);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
