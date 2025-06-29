namespace HouseholdBudgetCalculator.Models
{
    public class ProductName
    {
        public string Value { get; }

        public ProductName(string productName, string? prefix = null)
        {
            Value = prefix != null && productName.StartsWith(prefix) ? productName[prefix.Length..] : productName;
        }
        public override string ToString() => Value;
    }
}
