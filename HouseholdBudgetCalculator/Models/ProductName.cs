namespace HouseholdBudgetCalculator.Models
{
    public class ProductName(string productName, string? prefix = null)
    {
        public string Value { get; } = prefix != null && productName.StartsWith(prefix) ? productName[prefix.Length..] : productName;

        public override string ToString() => Value;
    }
}
