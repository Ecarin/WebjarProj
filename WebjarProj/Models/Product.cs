using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models;
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Image { get; set; }

    [Required]
    public PriceType PriceType { get; set; }

    [Required]
    public string Price { get; set; }

    public decimal? DiscountAmount { get; set; }

    public DateTime? DiscountExpireAt { get; set; }

    public List<Feature> Features { get; set; }

    public List<Addon> Addons { get; set; }

    [Required]
    public int Quantity { get; set; }
}

public enum PriceType
{
    CONSTANT,
    FORMULA
}
