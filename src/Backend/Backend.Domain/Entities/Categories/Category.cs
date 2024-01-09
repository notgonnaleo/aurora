using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Category
{
    [Table("Category")]
    public class Category
    {
        public Guid CategoryId { get; set; }
        public Guid TenantId { get; set; }  
        public string? CategoryName { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}

