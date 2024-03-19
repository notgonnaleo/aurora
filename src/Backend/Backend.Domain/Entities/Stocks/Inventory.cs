using Backend.Domain.Entities.Products;
using Backend.Domain.Enums.StockMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Stocks
{
    public class InventoryStockLog
    {
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        public int TotalAmount { get; set; }
        public int MovementStatusId { get; set; }
        public double Value { get; set; }
    }
    public class Inventory
    {
        public Product Product { get; set; }
        public ProductVariant Variant { get; set; }
        public int TotalAmount { get; set; }
        public MovementStatus Status { get; set; }
    }
}
