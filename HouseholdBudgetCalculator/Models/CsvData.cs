using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace HouseholdBudgetCalculator.Models
{
    public class CsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly? DateOfUse { get; set; }
        public ProductName ProductName { get; set; } = null!;
        public int TotalPaymentAmount { get; set; }
    }

    public class DateOnlyConverter : ITypeConverter
    {
        private static readonly string DATE_FORMAT = "yyyy/M/d";

        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (DateOnly.TryParseExact(text, DATE_FORMAT, out var dateOnly)) return dateOnly;
            return null;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return (value is DateOnly dateOnly) ? dateOnly.ToString(DATE_FORMAT, CultureInfo.InvariantCulture) : string.Empty;
        }
    }
}
