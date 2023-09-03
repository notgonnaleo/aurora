using Backend.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.Products.Taxes
{
    [Table("ProductTax")]
    public class ProductTax
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public int? CestId { get; set; }
        public int? NcmId { get; set; }
        public Guid? TributeId { get; set; }
        public Guid? CommercialTaxId { get; set; }
        public string? CEAN { get; set; } // EAN (Same thing.)
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
