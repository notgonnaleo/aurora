using Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Orders.Request;
using Backend.Domain.Enums.Orders;

namespace Backend.Domain.Entities.Orders
{
    [Table("Orders")]
    public class Order : Model
    {
        public Order() { }
        public Order(OrderRequest orderRequest)
        {
            var openingDate = DateTime.UtcNow;

            TenantId = orderRequest.TenantId;
            OrderId = Guid.NewGuid();
            OrderCode = GenerateOrderCode(openingDate);
            OrderOpeningDate = openingDate;
            OrderEstimatedDate = orderRequest.OrderEstimatedDate;
            OrderStatusId = (int)OrdersStatusEnums.Open;
            SellerId = orderRequest.SellerId;
            CustomerId = orderRequest.CustomerId;
            Active = true;
            Created = DateTime.UtcNow;
            CreatedBy = orderRequest.CustomerId;
            Updated = null;
            UpdatedBy = null;
            PaymentMethodId = orderRequest.PaymentMethodId;
            OrderTotalAmount = orderRequest.OrderTotalAmount;
            ParcelsQuantity = ValidateParcelQuantity(orderRequest.ParcelsQuantity);
        }

        private int ValidateParcelQuantity(int parcelQuantity)
        {
            if (parcelQuantity <= 0) 
                throw new ArgumentOutOfRangeException("Invalid parcel amount.");
            return parcelQuantity;
        }

        public string GenerateOrderCode(DateTime openingDate)
        {
            return $"{openingDate.Day}{openingDate.Month}{openingDate.Year}{new Random().Next(100, 1000)}";
        }

        [Required]
        public Guid TenantId { get; set; }
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public string OrderCode { get; set; }
        [Required]
        [ForeignKey("AgentId")]
        public Guid SellerId { get; set; }
        [Required]
        [ForeignKey("AgentId")]
        public Guid CustomerId { get; set; }
        [Required]
        public DateTime OrderOpeningDate { get; set; }
        [Required]
        public DateTime OrderEstimatedDate { get; set; }
        public DateTime? OrderEffectiveDate { get; set; }
        public int OrderStatusId { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public int ParcelsQuantity { get; set; }
        public int PaymentMethodId { get; set; }
        public Agent? Customer { get; set; }
        public Agent? Seller { get; set; }
    }

    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
    }
}
