using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.Products.Taxes.TributeSituationCodes
{
    [Table("TributeSituationCode")]
    public class TributeSituationCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } // https://cdmcontabilidade.com.br/tabela-cst-pis-cofins/
        public string Description { get; set; }
    }
}
