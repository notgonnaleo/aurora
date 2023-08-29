using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    public class ProductType
    {
        [Required]
        public Guid Id { get; set; }
        public string? Description { get; set; }
    }
}
