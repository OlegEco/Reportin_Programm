using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reportin_Programm.Models
{
    public class Warehouse : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        public List<Good>? Goods { get; set; }
    }
}