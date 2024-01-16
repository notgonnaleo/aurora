using Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Categories
{
    [Table("Category")]
    public class Category : Model
    {
        public Guid CategoryId { get; set; }
        public Guid TenantId { get; set; }  
        public string? CategoryName { get; set; }
    }
}

