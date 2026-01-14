namespace HouseholdBudgetCalculator.Models;

public enum ProductCategory
{
    Unknown,
    Food,
    Transport,
    Healthcare,
    Education,
    Clothing,
    DailyNecessities,
    Entertainment,
    Subscription,
    Mobile,
    Investment,
    Trip
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
