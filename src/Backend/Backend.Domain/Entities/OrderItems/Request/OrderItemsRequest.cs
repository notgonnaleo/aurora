using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.OrderItems.Request
{
    public class OrderItemsRequest
    {
        public Guid TenantId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? ItemVariantId { get; set; }
        public int ItemQuantity { get; set; }
    }
}
