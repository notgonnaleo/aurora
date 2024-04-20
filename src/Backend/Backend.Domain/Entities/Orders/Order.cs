using Backend.Domain.Entities.Payments;
using Backend.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Orders
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderOpeningDate { get; set; }
        public DateTime OrderEstimatedDate { get; set; }
        public DateTime OrderEffectiveDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal OrderTotalAmount { get; set; }
    }

    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string ÓrderStatusName { get; set; }
    }

    public class OrderItems
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Product Item { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemUnitAmount { get; set; }
        public decimal ItemTotalAmount { get; set; }
    }
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public Guid OrderPaymentId { get; set; }
        public decimal OrderPaymentTotalAmount { get; set; }
        public int OrderPaymentParcels { get; set; }
        public PaymentType PaymentType { get; set; }
    }

}
