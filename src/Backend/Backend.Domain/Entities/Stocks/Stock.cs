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
using Backend.Domain.Entities.Categories;
using Backend.Domain.Enums.StockMovements;

namespace Backend.Domain.Entities.Stocks
{
    [Table("Stock")]
    public class Stock : Model
    {
        [Key]
        public Guid StockMovementId { get; set; }
        [Required]
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime? MovementDate { get; set; }
        public int? MovementType { get; set; }

        //Chaves estrangeiras de outras tabelas 
        [ForeignKey("VariantId")]
        public Guid? VariantId { get; set; }

        [ForeignKey("AgentId")]
        public Guid AgentId { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class StockDetail : Model
    {
        public Guid StockMovementId { get; set; }
        public Guid TenantId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid? VariantId { get; set; }
        public string? VariantName { get; set; }
        public Guid AgentId { get; set; }
        public Guid AgentName { get; set; }
        public string SKU { get; set; }
        public string GTIN { get; set; }
        public double ProductValue { get; set; }
        public DateTime MovementDate { get; set; }
        public int Quantity { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public MovementTypes MovementType { get; set; }
        public MovementStatus MovementStatus { get; set; }
    }
}
