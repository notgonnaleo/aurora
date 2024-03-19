using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Categories;
using Backend.Domain.Enums.Colors;
using Backend.Domain.Enums.MetricUnits;
using Backend.Infrastructure.Enums.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    public class ProductVariant : Model
    {
        public ProductVariant() { }
        [Key]
        public Guid VariantId { get; set; }

        [Required]
        public Guid TenantId { get; set; }
        
        [Required]
        [ForeignKey("ProductId")]

        public Guid ProductId { get; set; }

        public string SKU { get; set; }
        public string GTIN { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ColorName { get; set; }

        public double? LiquidWeight { get; set; }
        public double? TotalWeight { get; set; }
        public double? Value { get; set; }

        public bool OverwriteValue { get; set; }

        public ProductVariant(ProductVariant productVariant, Guid userId)
        {
            VariantId = productVariant.VariantId;
            TenantId = productVariant.TenantId;
            ProductId = productVariant.ProductId;
            SKU = productVariant.SKU;
            GTIN = string.IsNullOrEmpty(productVariant.GTIN) ? productVariant.GTIN : "NO GTIN"; ;
            Name = productVariant.Name;
            Description = productVariant.Description;
            ColorName = productVariant.ColorName;
            LiquidWeight = productVariant.LiquidWeight;
            TotalWeight = productVariant.TotalWeight;
            Value = productVariant.Value;
            OverwriteValue = productVariant.OverwriteValue;
            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum language)
        {
            if (Value < 0)
            {
                throw new Exception(Localization.ProductValidations.ErrorProductNegativeValue(language));
            }
        }
    }


}
