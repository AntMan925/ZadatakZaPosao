using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.00, 1000000)]
        public decimal Price { get; set; }

        [Range(0, 1000)]
        public int Stock { get; set; }

        public bool IsActive { get; set; }
    }
}
