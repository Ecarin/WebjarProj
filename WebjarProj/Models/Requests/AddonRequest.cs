namespace WebjarProj.Models.Requests;

public class CreateAddonRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}

public class UpdateAddonRequest
{    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}