using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.SubCategories;
using Backend.Infrastructure.Enums.Localization;
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
        public Category() { }

        public Guid CategoryId { get; set; }
        public Guid TenantId { get; set; }  
        public string? CategoryName { get; set; }

        [NotMapped]
        public IEnumerable<SubCategory>? SubCategories { get; set; }

        public void ValidateFields(LanguagesEnum language)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                throw new Exception(Localization.CategoryValidations.ErrorCategoryMissingName(language));
            }
        }
    }
}

