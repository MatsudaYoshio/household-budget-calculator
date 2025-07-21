namespace HouseholdBudgetCalculator.Models;

// ProductName is currently a simple string wrapper, but it is implemented as a class for future extensibility.
public class ProductName(string name)
{
    public string Value { get; } = name ?? throw new ArgumentNullException(paramName: nameof(name), message: "Product name cannot be null.");
    public override string ToString() => Value;
}
