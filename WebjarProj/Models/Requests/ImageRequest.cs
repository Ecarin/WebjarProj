using System.ComponentModel.DataAnnotations;
namespace WebjarProj.Models.Requests;

public class ImageRequest
{
    /// <summary>
    /// Image as Base64
    /// </summary>
    [Required]
    public string Base64Image { get; set; }
}