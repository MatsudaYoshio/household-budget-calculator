namespace HouseholdBudgetCalculator.Models
{
    public class Product
    {
        public ProductName Name { get; set; } = null!;
        public int TotalPaymentAmount { get; set; }
        public ProductCategory Category { get; set; } = ProductCategory.Unknown;
    }
}
