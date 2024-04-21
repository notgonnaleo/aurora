using Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Orders
{
    [Table("Orders")]
    public class Order : Model
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public string OrderCode { get; set; }
        [Required]
        public DateTime OrderOpeningDate { get; set; }
        [Required]
        public DateTime OrderEstimatedDate { get; set; }
        public DateTime OrderEffectiveDate { get; set; }
        public int OrderStatusId { get; set; }
        public decimal OrderTotalAmount { get; set; }
    }

    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
    }

    [Table("OrderItems")]
    public class OrderItem : Model
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        public decimal? ItemTotalWeight { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemUnitAmount { get; set; }
        public decimal ItemTotalAmount { get; set; }

        public Order? Order { get; set; }
    }
}
