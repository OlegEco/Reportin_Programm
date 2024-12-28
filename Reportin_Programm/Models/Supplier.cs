using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Supplier : BaseEntity
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name must not exceed 100 characters")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address must not exceed 200 characters")] 
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City must not exceed 100 characters")] 
        public string City { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be in a valid format")]
        public string Phone { get; set; }

        public List<Warehouse>? Warehouses { get; set; } 
        public List<Good>? Goods { get; set; } 
    }
}