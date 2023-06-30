using System.Data;

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
    }
}