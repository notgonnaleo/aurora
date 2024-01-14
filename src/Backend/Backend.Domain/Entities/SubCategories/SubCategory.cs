using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Categories;


namespace Backend.Domain.Entities.SubCategories
{
    [Table("SubCategory")]
    public class SubCategory : Model
    {
        public Guid SubCategoryId { get; set; }
        public Guid TenantId { get; set; }
        public string? SubCategoryName { get; set; }
        public Guid CategoryId { get; set; }
    }
}
