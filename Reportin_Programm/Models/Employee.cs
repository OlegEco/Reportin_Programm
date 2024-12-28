using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The item name must be between 2 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$", ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must follow the standard format (e.g., example@domain.com)")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        public List<Warehouse>? Warehouses { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Supplier>? Suppliers { get; set; }
    }
}