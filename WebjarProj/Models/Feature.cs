using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebjarProj.Models;
public class Feature
{
    public int FeatureId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
