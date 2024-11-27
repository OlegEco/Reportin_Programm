namespace Reportin_Programm.Models
{
    public class Supplier : BaseEntity
    {
        public string Company { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Warehouse> Warehouses { get; set; }
        public List<Good> Goods { get; set; }
    }
}