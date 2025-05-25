// Using original namespace
namespace HouseholdBudgetCalculator.Models
{
    public class ProductName
    {
        private const string PAY_PAY_PREFIX = "ＰａｙＰａｙ　"; // Full-width space
        public string Value { get; }

        public ProductName(string productName)
        {
            // Ensure productName is not null to prevent NullReferenceException on StartsWith
            if (productName == null)
            {
                // Or throw ArgumentNullException, or set Value to null or empty.
                // Based on its usage in CsvData, it's non-nullable (null!), so an exception might be better.
                // However, to match original behavior if it was lenient:
                Value = string.Empty; 
            }
            else
            {
                Value = productName.StartsWith(PAY_PAY_PREFIX) ? productName.Substring(PAY_PAY_PREFIX.Length) : productName;
            }
        }

        public override string ToString() => Value;

        // It's good practice to override Equals and GetHashCode if overriding ToString or if objects are used in collections/comparisons.
        public override bool Equals(object obj)
        {
            if (obj is ProductName other)
            {
                return Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }
}
