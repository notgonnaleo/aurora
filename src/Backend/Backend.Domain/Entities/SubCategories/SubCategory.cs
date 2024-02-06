using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Categories;


namespace Backend.Domain.Entities.SubCategories
{
    [Table("SubCategory")]
    public class SubCategory : Model
    {
        public Guid TenantId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }

        public Guid CategoryId { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
