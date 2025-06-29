using CsvHelper.Configuration.Attributes;
using HouseholdBudgetCalculator.Services;

namespace HouseholdBudgetCalculator.Models
{
    public interface ICsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))]
        DateOnly? DateOfUse { get; set; }
        ProductName ProductName { get; set; }
        int TotalPaymentAmount { get; set; }
        CsvFormatDefinition FormatDefinition { get; } // プロパティ名を変更しました
    }
}
