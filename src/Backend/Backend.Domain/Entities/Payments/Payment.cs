using Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Payments
{
    [Table("Payments")]
    public class Payment : Model
    {
        [Key]
        public Guid PaymentId { get; set; }
        public Guid PayerId { get; set; }
        public Guid SellerId { get; set; }
        public int TotalParcels { get; set; }
        public int PaymentStatusId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentEffectiveDate { get; set; }
        public DateTime PaymentEstimatedDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    [Table("Parcels")]
    public class Parcel : Model
    {
        [ForeignKey("PaymentId")]
        public Guid PaymentId { get; set; }
        [Key]
        public Guid ParcelId { get; set; }
        public DateTime ParcelEffectiveDate { get; set; }
        public DateTime ParcelEstimatedDate { get; set; }
        public decimal ParcelAmount { get; set; }
        public int Sequence { get; set; }
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
