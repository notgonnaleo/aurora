using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.Products.Taxes.Tributes
{
    public class Tribute
    {
        [Key]
        public int TributeId { get; set; }
        public int TributeMetricId { get; set; } // uTrib - KG
        public decimal TributeQuantity { get; set; } // qTrib - Item quantity * weight per item 
        public decimal TributeValue { get; set; } // vTrib - Value
    }
}
