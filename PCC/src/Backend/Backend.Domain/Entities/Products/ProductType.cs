using Backend.Domain.Entities.Taxes.ICMS;
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
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ICMSId { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
