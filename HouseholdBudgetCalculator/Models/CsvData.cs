using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace HouseholdBudgetCalculator.Models
{
    public class CsvData
    {
        [Name("利用日/キャンセル日")]
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly? DateOfUse { get; set; }

        [Name("利用店名・商品名")]
        [TypeConverter(typeof(ProductNameConverter))]
        public ProductName ProductName { get; set; } = null!;

        [Name("支払総額")]
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

    public class ProductNameConverter : ITypeConverter
    {
        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text is not null ? new ProductName(text) : null;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return value is ProductName productName ? productName.ToString() : string.Empty;
        }
    }
}
