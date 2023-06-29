using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}