using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products.Request
{
    public class ProductRequest
    {
        public Product ProductInfo { get; set; }
        public ProductMedia Media { get; set; }
    }
}
