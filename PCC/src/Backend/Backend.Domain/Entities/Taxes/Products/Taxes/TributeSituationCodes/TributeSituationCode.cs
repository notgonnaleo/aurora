using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.Products.Taxes.TributeSituationCodes
{
    public class TributeSituationCode
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; } // https://cdmcontabilidade.com.br/tabela-cst-pis-cofins/
        public string Description { get; set; }
    }
}
