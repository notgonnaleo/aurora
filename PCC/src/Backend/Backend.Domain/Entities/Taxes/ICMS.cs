using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes
{
    public class ICMS
    {
        public Guid Id { get; set; }
        public string? State { get; set; }
        public decimal? TaxValue { get; set; }
    }
}
