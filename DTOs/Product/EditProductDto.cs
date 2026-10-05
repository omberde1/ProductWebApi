using ProductWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.DTOs.Product
{
    public class EditProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(99, 99999)]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }
        [EnumDataType(typeof(ProductCategory))]
        public ProductCategory Category { get; set; } = ProductCategory.NA;
    }
}
