using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.SubCategories;
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

        [NotMapped]
        public IEnumerable<SubCategory>? SubCategories { get; set; }

        public Category Create(Category category, Guid userId)
        {
            return new Category()
            {
                TenantId = category.TenantId,
                CategoryId = Guid.NewGuid(),
                CategoryName = category.CategoryName,
                CreatedBy = userId,
                Created = DateTime.UtcNow,
                Updated = null,
                UpdatedBy = null,
                Active = true,
            };
        }

        public void ValidateFields()
        {
            if (CategoryName is null || CategoryName == string.Empty)
            {
                throw new Exception("Cannot create or update a category without specifying a name.");
            }
        }
    }
}

