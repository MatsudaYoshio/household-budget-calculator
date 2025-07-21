using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace HouseholdBudgetCalculator.Models.Conveters;

public class ProductNameConverter: DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text)) return null;
        return new ProductName(text);
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value is ProductName productName ? productName.ToString() : string.Empty;
    }
}
