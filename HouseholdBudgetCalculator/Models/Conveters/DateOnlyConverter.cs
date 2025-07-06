using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace HouseholdBudgetCalculator.Models.Conveters
{
    public class DateOnlyConverter : ITypeConverter
    {
        private static readonly string DATE_FORMAT = "yyyy/M/d";

        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (DateOnly.TryParseExact(text, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateOnly)) return dateOnly;
            // TryParseExactが失敗した場合、nullではなく例外をスローするか、エラー処理を検討する
            // ここでは一旦 null を返す（CsvHelperのデフォルトの挙動に近い）
            return null;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return value is DateOnly dateOnly ? dateOnly.ToString(DATE_FORMAT, CultureInfo.InvariantCulture) : string.Empty;
        }
    }
}
