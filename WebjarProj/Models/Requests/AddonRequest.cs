using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models.Requests;

public class CreateAddonRequest
{
    /// <summary>
    /// Name of Addon
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Price of Addon
    /// </summary>
    [Required]
    public decimal Price { get; set; }
}

public class UpdateAddonRequest
{
    /// <summary>
    /// Name of Addon
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Price of Addon
    /// </summary>
    [Required]
    public decimal Price { get; set; }
}