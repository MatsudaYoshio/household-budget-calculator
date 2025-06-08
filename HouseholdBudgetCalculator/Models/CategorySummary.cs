using System; // For DateOnly?

public class CategorySummary
{
    public string CategoryName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public DateOnly? DateOfUse { get; set; } // Changed from DateTime? to DateOnly?
    public int TotalAmount { get; set; }
}
