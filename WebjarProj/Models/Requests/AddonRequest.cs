namespace WebjarProj.Models.Requests;

public class CreateAddonRequest
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
}

public class UpdateAddonRequest : Addon
{

}