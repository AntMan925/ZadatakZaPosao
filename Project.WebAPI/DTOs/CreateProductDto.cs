using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength (100)]
        public string Name { get; set; }

        [Range (0.00, 1000000)]
        public decimal Price { get; set; }
        [Range(0, 1000)]
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
