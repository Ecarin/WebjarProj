namespace WebjarProj.Models.Requests;

public class ImageRequest
{
    [Required]
    public string Base64Image { get; set; }
}