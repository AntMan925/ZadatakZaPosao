using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.DTOs
{
    public class UpdateProductCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
