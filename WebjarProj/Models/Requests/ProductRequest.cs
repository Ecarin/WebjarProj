using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebjarProj.Models.Requests
{
    public class ProductRequest
    {
        /// <summary>
        /// Name of Product
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Enter returned value from [POST /api/Images] as Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// PriceType only can be "FORMULA" or "CONSTANT"
        /// </summary>
        [Required]
        public string PriceType { get; set; }

        /// <summary>
        /// Normall price or Complex Formulas like "2*4-$DOLLAR*(10/2)" is supported.
        /// </summary>
        [Required]
        public string Price { get; set; }

        /// <summary>
        /// DiscountAmount can be Null or Price as number
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// DiscountExpireAt can be Null to be forever or Date for Limited usage
        /// </summary>
        public DateTime? DiscountExpireAt { get; set; }

        /// <summary>
        /// Quantity of Product
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}