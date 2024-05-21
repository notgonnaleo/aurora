using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Backend.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Entities.Products;

public class ProductMedia : Model
{
    [Required]
    public Guid TenantId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Key]
    public Guid Id { get; set; }
    public ProductMediaType MediaType { get; set; }

    /// <summary>
    /// Image buffer data converted to Base64 string.
    /// </summary>
    [NotMapped]
    public string? ImageBuffer { get; set; }
    public string? MediaURL { get; set; }

    public uint Priority { get; set; } // This is just to make it easier to sort the product media, the higher the number the more importance will be given to this media
    [JsonIgnore]
    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

}