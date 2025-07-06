namespace HouseholdBudgetCalculator.Models
{
    //今は単純な文字列ラッパーだが、将来の拡張性を考慮してクラス化
    public class ProductName(string name)
    {
        public string Value { get; } = name;
        public override string ToString() => Value;
    }
}
