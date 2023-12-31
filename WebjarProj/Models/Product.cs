﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Image { get; set; }

    [Required]
    public string PriceType { get; set; }

    [Required]
    public string Price { get; set; }

    public decimal? DiscountAmount { get; set; }

    public DateTime? DiscountExpireAt { get; set; }

    [Required]
    public int Quantity { get; set; }

    public ICollection<ProductFeature> ProductFeatures { get; set; }
}