using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.OrderHistories.Response
{
    public class OrderMovementEntryHistoryResponse
    {
        public Guid OrderHistoryId { get; set; }
        public Guid OrderId { get; set; }
        public int OrderMovementType { get; set; }
        public int OrderTotalItemsMovement { get; set; }
        public ItemThumbnail Item { get; set; }
        public AgentDetail From { get; set; }
        public AgentDetail To { get; set; }
        public DateTime MovementTimestamp { get; set; }
    }
}
