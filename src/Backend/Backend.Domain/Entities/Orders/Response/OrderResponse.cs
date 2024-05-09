using Backend.Domain.Entities.Agents.Response;
using Backend.Domain.Entities.OrderItems.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Orders.Response
{
    public class OrderResponse
    {
        public Guid TenantId { get; set; }
        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int? TotalParcels { get; set; }
        public decimal? OrderParcelAmount { get; set; }
        public decimal? OrderTotalAmount { get; set; }
        public IEnumerable<OrderItemsResponse>? OrderItems { get; set; }

        public DateTime OrderOpeningDate { get; set; }
        public DateTime OrderEstimatedDate { get; set; }
        public DateTime OrderEffectiveDate { get; set; }
    }

    public class OrderOpeningConfirmation
    {
        public Guid TenantId { get; set; }
        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderOpeningDate { get; set; }
        public DateTime OrderEstimatedDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public EmployeeThumbnail Seller { get; set; }
        public CustomerThumbnail Customer { get; set; }
    }
}
