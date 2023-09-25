using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.CNAE
{
    [Table("CNAE")]
    public class CNAE
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
    }
}
