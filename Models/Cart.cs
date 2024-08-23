using System.ComponentModel.DataAnnotations;

namespace MVC_Addtocartdummy.Models
{
    public class Cart
    {
        [Key]
        public int Pid { get; set; }
        public string? Pname { get; set; }
        public string? Pcat { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }
        public string? suser {  get; set; }
    }
}
