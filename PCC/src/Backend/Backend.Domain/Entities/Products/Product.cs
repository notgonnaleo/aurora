using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    [Table("Product")]
    public class Product
    {
        [Required]
        public Guid TenantId { get; set; }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? SKU { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("ProductType")]
        public Guid? ProductTypeId { get; set; }
        public decimal Value { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? LiquidWeight { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }

        [JsonIgnore]
        public virtual ProductType? ProductType { get; set; }
    }
}
