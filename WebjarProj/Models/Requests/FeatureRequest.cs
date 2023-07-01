namespace WebjarProj.Models.Requests;

public class CreateFeatureRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Value { get; set; }
}
public class UpdateFeatureRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Value { get; set; }
}