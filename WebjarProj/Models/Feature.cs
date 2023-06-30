using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebjarProj.Models;
public class Feature
{
    [Key]
    public int FeatureId { get; set; }

    [Required]
    public string Name { get; set; }    
    
    [Required]
    public string Value { get; set; }


    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
