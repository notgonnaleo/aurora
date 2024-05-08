using Backend.Domain.Entities.OrderItems.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Orders.Request
{
    public class OrderRequest
    {
        public Guid TenantId { get; set; }
        public Guid SellerId { get; set; }
        public Guid CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public int ParcelsQuantity { get; set; }
        public DateTime OrderEstimatedDate { get; set; }
        public IEnumerable<OrderItemsRequest>? OrderItems { get; set; }
    }
}
