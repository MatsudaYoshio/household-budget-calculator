using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace HouseholdBudgetCalculator.Models.Conveters;

public class DateOnlyConverter: ITypeConverter
{
    private static readonly string _dateFormat = "yyyy/M/d";

    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text)) return null;
        if (DateOnly.TryParseExact(
            s: text,
            format: _dateFormat,
            provider: CultureInfo.InvariantCulture,
            style: DateTimeStyles.None,
            result: out var dateOnly)
            )
        {
            return dateOnly;
        }
        throw new TypeConverterException(
            typeConverter: this,
            memberMapData: memberMapData,
            text: text,
            context: row.Context,
            message: $"The string '{text}' is not a valid date in the format '{_dateFormat}'."
        );
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value is DateOnly dateOnly ? dateOnly.ToString(format: _dateFormat, provider: CultureInfo.InvariantCulture) : string.Empty;
    }
}
