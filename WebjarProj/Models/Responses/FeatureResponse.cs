namespace WebjarProj.Models.Responses;

public class SingleFeatureResponse : ResultDTO
{
    public Feature Feature { get; set; }
}
public class FeaturesResponse : ResultDTO
{
    public List<Feature> Features { get; set; }
}