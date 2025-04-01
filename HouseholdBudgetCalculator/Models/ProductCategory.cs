namespace HouseholdBudgetCalculator.Models
{
    public enum ProductCategory
    {
        Unknown,
        Food,
        Special
    }

    public static class ProductCategoryExtensions
    {
        public static ProductCategory ConvertToEnum(string category)
        {
            if (Enum.TryParse<ProductCategory>(category, true, out ProductCategory result))
            {
                return result;
            }
            throw new ArgumentException($"Invalid category: {category}");
        }
    }
}
