using System.Data;
using WebjarProj.Models;
using WebjarProj.Models.Responses;

namespace WebjarProj.Utility
{
    public class Helper
    {
        public static string CalculatePriceFromFormula(string formula, decimal dollarValue)
        {
            // Replace the $DOLLAR placeholder with the actual value
            formula = formula.Replace("$DOLLAR", dollarValue.ToString());

            // Create a DataTable and compute the expression
            DataTable dataTable = new DataTable();
            var result = dataTable.Compute(formula, "");

            return result.ToString() ?? "0";
        }

        public static async Task<decimal> CalculateProductFinalPrice(CustomProductResult product, decimal dollarValue)
        {
            decimal Price;
            switch (product.PriceType)
            {
                case "CONSTANT":
                    Price = decimal.Parse(product.Price);
                    break;

                case "FORMULA":
                    Price = decimal.Parse(CalculatePriceFromFormula(product.Price, dollarValue));
                    break;

                default:
                    Price = 0;
                    break;
            }

            if (product.DiscountAmount is not null &&
            (product.DiscountExpireAt >= DateTime.UtcNow || product.DiscountExpireAt is null))
            {
                product.FinalPrice = Price - product.DiscountAmount ?? 0;
            }
            else
            {
                product.FinalPrice = Price;
            }
            return product.FinalPrice;
        }
    }
}