using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models
{
    public class Addon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}