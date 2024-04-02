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
        public int ProductTypeId { get; set; }
        public string ColorName { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SKUValue { get; set; }

        public SKU(string productName, int productTypeId, string colorName)
        {
            string Year = DateTime.Now.Year.ToString();
            ProductName = productName.Substring(0, 3);
            ProductTypeId = productTypeId;
            //CategoryName = category.Substring(0, 1);
            //SubCategoryName = subCategory.Substring(0, 1);
            ColorName = colorName.Substring(0, 1);
            string numberId = new Random().Next(10, 100).ToString();
            SKUValue = $"{Year}{ProductName.ToUpper()}{ProductTypeId}{numberId}";
        }
    }
}
