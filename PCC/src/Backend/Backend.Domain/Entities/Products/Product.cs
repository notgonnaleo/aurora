using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    [Table("Product")]
    public class Product
    {
        [Required]
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
        public int? CestId { get; set; }
        public int? NcmId { get; set; }
        public string? cEAN { get; set; }
        public string? cEANTax { get; set; }
        public int? TaxUnitId { get; set; }

        [Required]
        public virtual ProductType ProductTypes { get; set; }
    }
}
