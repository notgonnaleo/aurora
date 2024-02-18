using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    public class SKU
    {
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string ColorName { get; set; }
        public string  UnitName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}
