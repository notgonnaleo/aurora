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
using Backend.Infrastructure.Enums.Localization;

namespace Backend.Domain.Entities.SubCategories
{
    [Table("SubCategory")]
    public class SubCategory : Model
    {
        public SubCategory() { }

        public Guid TenantId { get; set; }
        public Guid SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }

        public Guid CategoryId { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public SubCategory(SubCategory subCategory, Guid userId)
        {
            SubCategoryId = Guid.NewGuid();
            TenantId = subCategory.TenantId;
            SubCategoryName = subCategory.SubCategoryName;
            CategoryId = subCategory.CategoryId;
            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum language)
        {
            if (CategoryId.Equals(Guid.Empty))
            {
                throw new Exception(Localization.SubCategoryValidations.ErrorSubCategoryMissingParentCategory(language));
            }
        }
    }
}

