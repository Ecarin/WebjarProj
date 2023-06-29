namespace WebjarProj.Models.Responses;

public class SingleAddonResponse : ResultDTO
{
    public Addon Addon {get; set;}
}
public class AddonsResponse : ResultDTO
{
    public List<Addon> Addons {get; set;}
}