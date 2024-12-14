using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        public List<Warehouse>? Warehouses { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Supplier>? Suppliers { get; set; }
    }
}