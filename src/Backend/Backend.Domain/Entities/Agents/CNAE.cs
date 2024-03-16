using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agents
{
    public class CNAE
    {
        public string? CnaeId { get; set; }
        public string? CnaeDescription { get; set; }
        public double? Aliquot { get; set; }
        public int? Annex { get; set; }
        public bool? FactorR { get; set; }
    }
}
