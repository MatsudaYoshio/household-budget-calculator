using System;

public class CategorySummary
{
    public DateOnly? DateOfUse { get; set; } = null;
    public string CategoryName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int TotalAmount { get; set; }
}
