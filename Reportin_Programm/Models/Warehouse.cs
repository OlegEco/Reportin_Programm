namespace Reportin_Programm.Models
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public string Adress { get; set; }

        public List<Good> Goods { get; set; }
    }
}