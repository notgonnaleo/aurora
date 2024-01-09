using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.ProductTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Products
{
    [Table("Product")]
    public class Product : Model
    {
        [Required]
        public Guid TenantId { get; set; }
        [Key]
        public Guid Id { get; set; }
        public int ProductTypeId { get; set; }
        [Required]
        public string SKU { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? LiquidWeight { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType? ProductType { get; set; }


        public Product Create(Product product, Guid userId)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                TenantId = product.TenantId,
                SKU = product.SKU, 
                Name = product.Name,
                Description = product.Description,
                ProductTypeId = product.ProductTypeId,
                Value = product.Value,
                TotalWeight = product.TotalWeight,
                LiquidWeight = product.LiquidWeight,
                CreatedBy = userId,
                Created = DateTime.UtcNow,
                Updated = null,
                UpdatedBy = null,
                Active = true
            };
        }

        public Product Update(Product product, Guid userId)
        {
            return new Product() // Updating the header info from the product.
            {
                TenantId = product.TenantId,
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                ProductTypeId = product.ProductTypeId,
                Active = true,
                Updated = DateTime.Now,
                UpdatedBy = userId
            };
        }
    }

    public class ProductDetail : Product
    {
        public string ProductTypeName { get; set; }
        // Categories properties are the next
    }
}
