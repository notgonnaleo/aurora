using Backend.Domain.Entities.Agents.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Notifications
{
    public class Notification
    {
        /// <summary>
        /// Entity id that will be used for redirect reference (Order, Product, Agent or Inventory)
        /// </summary>
        public Guid NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public string? NotificationName { get; set; }
        public string? NotificationDescription { get; set; }
        public string? NotificationItemCode { get; set; }
        
        /// <summary>
        /// Product, Agent Type Id
        /// </summary>
        public int? NotificationItemTypeId { get; set; }
        /// <summary>
        /// Order and inventory status Id
        /// </summary>
        public int? NotificationItemStatusId { get; set; }

        public DateTime NotificationTimestamp { get; set; }
        /// <summary>
        /// Created By OR Updated By Field
        /// </summary>
        public AgentThumbnailResponse NotificationActor { get; set; }
    }
}
