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
        public string? MovementType { get; set; }
        public int MovementStatusId { get; set; }



        //Chaves estrangeiras de outras tabelas 
        [ForeignKey("VariantId")]
        public Guid VariantId { get; set; }

        [ForeignKey("AgentId")]
        public Guid AgentId { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
    }

    public class StockDetail : Stock
    {
        //public Guid CategoryId { get; set; }
        //public string CategoryName { get; set; }

        //public Guid SubCategoryId { get; set; }
        //public string SubCategoryName { get; set; }

        //public string SKU { get; set; }
        //public string GTIN { get; set; }

        public string ProductName { get; set; }
        //public double ProductValue { get; set; }

        //public string? VariantName { get; set; }

        //public string AgentName { get; set; }

        /*
            "categoryName": "Eletronic",
            "subCategoryName": "Smartphone",
            "productTypeName": "Product",
            "sku": "202401",
            "gtin": "012345678910111213",
            "name": "Samsung Galaxy S4",
            "colorName": null,
            "metricUnitName": null,
            "value": 604.99,
            "totalWeight": 0.13,
            "liquidWeight": 0.13,
            "productTypeId": 3,
            "categoryId": "63cf51c6-e90e-4725-b6c3-1c40986d6847",
            "subCategoryId": "cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6",
         */
    }
}
