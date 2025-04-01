namespace HouseholdBudgetCalculator.Models
{
    public class ProductName(string productName)
    {
        private const string PAY_PAY_PREFIX = "ＰａｙＰａｙ　";
        public string Value { get; } = productName.StartsWith(PAY_PAY_PREFIX) ? productName[PAY_PAY_PREFIX.Length..] : productName;
        public override string ToString() => Value;
    }
}
