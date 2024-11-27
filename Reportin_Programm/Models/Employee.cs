using Microsoft.AspNetCore.Identity;

namespace Reportin_Programm.Models
{
    public class Employee : BaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Warehouse> Warehouses { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}