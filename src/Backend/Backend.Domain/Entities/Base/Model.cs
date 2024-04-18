using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Base
{
    public class Model
    {
        [NotMapped]
        public int Skip { get; set; }
        [NotMapped]
        public int Take { get; set; }
        [NotMapped]
        public int TotalRowCount { get; set; }

        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
