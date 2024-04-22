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
}
