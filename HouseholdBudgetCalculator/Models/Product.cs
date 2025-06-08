namespace HouseholdBudgetCalculator.Models
{
    public class Product
    {
        public ProductName Name { get; set; } = null!;
        public DateTime? DateOfUse { get; set; }
        public int TotalPaymentAmount { get; set; }
        public ProductCategory Category { get; set; } = ProductCategory.Unknown;
    }
}
