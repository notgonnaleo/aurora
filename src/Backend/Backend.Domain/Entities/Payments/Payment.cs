using Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Orders;

namespace Backend.Domain.Entities.Payments
{
    [Table("Payments")]
    public class Payment : Model
    {
        [Key]
        public Guid PaymentId { get; set; }
        [ForeignKey("OrderId")]
        public Guid? OrderId { get; set; }
        [ForeignKey("AgentId")]
        public Guid CustomerId { get; set; }
        [ForeignKey("AgentId")]
        public Guid SellerId { get; set; }
        public int TotalParcels { get; set; }
        public int PaymentStatusId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentEffectiveDate { get; set; }
        public DateTime PaymentEstimatedDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Agent? Customer { get; set; }
        public Agent? Seller { get; set; }
        public Order? Order { get; set; }
    }

    [Table("Parcels")]
    public class Parcel : Model
    {
        [Key]
        public Guid ParcelId { get; set; }
        [ForeignKey("PaymentId")]
        public Guid PaymentId { get; set; }
        public DateTime ParcelEffectiveDate { get; set; }
        public DateTime ParcelEstimatedDate { get; set; }
        public decimal ParcelAmount { get; set; }
        public int Sequence { get; set; }

        public Payment? Payment { get; set; }
    }

    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; }
    }

    public class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
