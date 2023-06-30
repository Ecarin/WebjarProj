using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebjarProj.Models;
public class ProductFeature
{
    [JsonIgnore]
    public int ProductFeatureId { get; set; }

    [JsonIgnore]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }

    [ForeignKey("Feature")]
    public int FeatureId { get; set; }
    public Feature Feature { get; set; }
}