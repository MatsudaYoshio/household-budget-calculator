namespace HouseholdBudgetCalculator.Models
{
    public enum ProductCategory
    {
        Unknown,
        Food,
        Transport
    }

    public static class ProductCategoryExtensions
    {
        public static ProductCategory ConvertToEnum(string category)
        {
            if (Enum.TryParse(category, true, out ProductCategory result))
            {
                return result;
            }
            throw new ArgumentException($"Invalid category: {category}");
        }
    }
}
