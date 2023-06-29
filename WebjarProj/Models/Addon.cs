using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models;
public class Addon
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }
}
