using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    public class ProductVariant : Model
    {
        public Guid VariantId { get; set; }
        public Guid TenantId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
