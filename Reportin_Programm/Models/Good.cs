using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Good : BaseEntity
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "The name must not exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        public double Price { get; set; }

        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,!?'-]*$", ErrorMessage = "Description contains invalid characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category name must not exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can only contain letters and spaces.")]
        public string Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The quantity must be non-negative.")]
        public int Quantity { get; set; }
    }
}