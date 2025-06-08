using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion; // Ensure this is present for DateOnlyConverter

namespace HouseholdBudgetCalculator.Models
{
    public class CsvData
    {
        [Name("利用店名・商品名")]
        [TypeConverter(typeof(ProductNameConverter))]
        public ProductName ProductName { get; set; } = null!;

        [Name("利用日/キャンセル日")]
        [TypeConverter(typeof(CustomDateOnlyConverter))] // Changed to CustomDateOnlyConverter
        public DateOnly? DateOfUse { get; set; } // Changed to DateOnly?

        [Name("支払総額")]
        public int TotalPaymentAmount { get; set; }
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

    public class CustomDateOnlyConverter : DateOnlyConverter // Inherit from CsvHelper's DateOnlyConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            // Standard format expected "yyyy/M/d"
            if (DateOnly.TryParseExact(text, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly result))
            {
                return result;
            }
            // Fallback for "yyyy/MM/dd"
            if (DateOnly.TryParseExact(text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            // Fallback for "MM/dd/yyyy" - consider if your CSV might have this
            if (DateOnly.TryParseExact(text, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
             if (DateOnly.TryParseExact(text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Add other formats if necessary, e.g., "yyyy-MM-dd"
            if (DateOnly.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // If strict parsing is required and no format matches, CsvHelper's default behavior
            // when a converter returns null for a non-nullable type (if DateOnly? wasn't nullable)
            // would be to throw a TypeConverterException. For DateOnly?, returning null is fine.
            // You could also log a warning here if desired, via means external to this method.
            return null;
        }

        // Override ConvertToString if you need to write DateOnly? back to CSV in a specific format.
        // The default DateOnlyConverter.ConvertToString might use "yyyy-MM-dd".
        // If you need "yyyy/MM/dd", you can override:
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is DateOnly dateOnlyValue)
            {
                return dateOnlyValue.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            return base.ConvertToString(value, row, memberMapData); // Or simply string.Empty for nulls
        }
    }
}
