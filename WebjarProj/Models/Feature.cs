using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebjarProj.Models;
public class Feature
{
    //[JsonIgnore]
    public int FeatureId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
