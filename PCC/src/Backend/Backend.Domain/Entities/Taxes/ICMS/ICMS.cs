using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.ICMS
{
    [Table("ICMS")]
    public class ICMS
    {
        [Key]
        public Guid Id { get; set; }
        public string? State { get; set; }
        public string? ProductType { get; set; }
        public decimal? TaxValue { get; set; }
    }
}
