using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? SKU { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ProductTypeId { get; set; }
        public decimal Value { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? LiquidWeight { get; set; }
        public int? CestId { get; set; }
        public int? NcmId { get; set; }
        public string? cEAN { get; set; }
        public string? cEANTax { get; set; }
        public int? TaxUnitId { get; set; }
    }
}
