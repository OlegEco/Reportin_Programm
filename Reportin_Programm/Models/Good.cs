namespace Reportin_Programm.Models
{
    public class Good : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}