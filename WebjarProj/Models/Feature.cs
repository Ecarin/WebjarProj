using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models;
public class Feature
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }    
    
    [Required]
    public string Value { get; set; }
}
