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
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SKUValue { get; set; }

        public SKU(string productName, string productTypeName, string colorName)
        {
            ProductName = productName.Substring(0, 1);
            ProductTypeName = productTypeName.Substring(0, 1);
            //CategoryName = category.Substring(0, 1);
            //SubCategoryName = subCategory.Substring(0, 1);
            ColorName = colorName.Substring(0, 3);
            string numberId = new Random().Next(10, 100).ToString();
            SKUValue = $"{ProductName}{ProductTypeName}{ColorName}{numberId}";
        }
    }
}
