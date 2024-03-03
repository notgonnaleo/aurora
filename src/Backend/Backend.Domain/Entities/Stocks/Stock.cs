using Backend.Domain.Entities.Products;
using Backend.Domain.Entities.Agents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Base;

namespace Backend.Domain.Entities.Stock
{
    [Table("Stock")]
    public class Stock : Model
    {
        [Key]
        public Guid StockMovementId { get; set; }
        [Required]
        public Guid TenantId { get; set; }
        [Required]      
        public Guid VariantId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? MovementDate { get; set; }
        public string? MovementType { get; set; }
        public int MovementStatusId { get; set; }


        public Guid Id { get; set; }
        [ForeignKey("Id")]
        public virtual Agent? Agent { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
