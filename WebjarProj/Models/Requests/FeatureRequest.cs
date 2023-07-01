using System.ComponentModel.DataAnnotations;
namespace WebjarProj.Models.Requests;

public class CreateFeatureRequest
{
    /// <summary>
    /// Name of Feature
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Value of Feature
    /// </summary>
    [Required]
    public string Value { get; set; }
}
public class UpdateFeatureRequest
{
    /// <summary>
    /// Name of Feature
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Value of Feature
    /// </summary>
    [Required]
    public string Value { get; set; }
}