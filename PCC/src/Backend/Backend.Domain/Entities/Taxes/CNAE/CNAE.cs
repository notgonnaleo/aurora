using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Taxes.CNAE
{
    public class CNAE
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
    }
}
