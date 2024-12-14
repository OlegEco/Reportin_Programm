using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm
{
    public class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext() { }

        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
