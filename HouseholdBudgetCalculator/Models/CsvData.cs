using CsvHelper.Configuration.Attributes;
using HouseholdBudgetCalculator.Models.Conveters;

namespace HouseholdBudgetCalculator.Models
{
    public interface CsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))]
        DateOnly? DateOfUse { get; set; }
        ProductName ProductName { get; set; }
        int TotalPaymentAmount { get; set; }
    }
}
