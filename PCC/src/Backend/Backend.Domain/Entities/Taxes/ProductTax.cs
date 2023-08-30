using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes
{
    public class ProductTax
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int? CestId { get; set; }
        public int? NcmId { get; set; }
        public string? CEAN { get; set; }
        public int? TaxUnitId { get; set; } // uTrib
        public int? SalesUnit { get; set; } // uCom
        public int? SalesQuantity { get; set; } // qCom
        public decimal? AliquotValue { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
