using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using HouseholdBudgetCalculator.Services; // CsvFormatDefinition を使うため

namespace HouseholdBudgetCalculator.Models
{
    public interface ICsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))] // この属性は具象クラス側に持たせるべきか検討
        DateOnly? DateOfUse { get; set; }
        ProductName ProductName { get; set; }
        int TotalPaymentAmount { get; set; }
        CsvFormatDefinition FormatDefinition { get; } // プロパティ名を変更しました
    }
}
