using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Base
{
    public class Model
    {
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
