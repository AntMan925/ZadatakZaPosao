using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.DTOs
{
    public class ProductCategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 

        public string Description { get; set; }

    }
}
