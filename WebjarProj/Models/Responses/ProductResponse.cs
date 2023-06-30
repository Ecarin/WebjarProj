namespace WebjarProj.Models.Responses;

public class SingleProductResponse : ResultDTO
{
    public CustomProductResult Product { get; set; }
    public List<Addon>? addons{ get; set; }
}
public class ProductsResponse : ResultDTO
{
    public List<CustomProductResult> Products { get; set; }
}
public class CustomProductResult : Product
{
    public decimal FinalPrice { get; set; }
}