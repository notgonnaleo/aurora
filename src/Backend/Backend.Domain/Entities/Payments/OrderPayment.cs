using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Payments
{
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public Guid OrderPaymentId { get; set; }
        public decimal OrderPaymentTotalAmount { get; set; }
        public int OrderPaymentParcels { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid PayerId { get; set; }
        public Guid SellerId { get; set; }
        public DateTime PaymentEffectiveDate { get; set; }
        public DateTime PaymentETA { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
