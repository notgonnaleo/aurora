using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.Products.Taxes.Commercial
{
    public class CommercialTax
    {
        [Key]
        public Guid CommercialTaxId { get; set; } // uCom - Identity in the system
        public Guid? CommercialTaxMetricId { get; set; } // uCom - Metric Unity
        public int? CommercialTaxQuantity { get; set; } // qCom - Quantity
        public decimal? CommercialTaxValue { get; set; } // vCom - Value
    }
}
