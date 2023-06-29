using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebjarProj.Models;
public class Product_Feature
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey("Feature")]
    public int FeatureId { get; set; }
    public Feature Feature { get; set; }
}
