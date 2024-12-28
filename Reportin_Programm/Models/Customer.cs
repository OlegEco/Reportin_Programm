using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must follow the standard format (e.g., example@domain.com)")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }
        public Employee? Employees { get; set; }
    }
}
