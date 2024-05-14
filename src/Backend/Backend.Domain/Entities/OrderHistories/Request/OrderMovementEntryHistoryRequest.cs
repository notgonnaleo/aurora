using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.OrderHistories.Request
{
    public class OrderMovementEntryHistoryRequest
    {
        public Guid OrderHistoryId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid OrderMovementType { get; set; }
        public int OrderTotalItemsMovement { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
    }
}
