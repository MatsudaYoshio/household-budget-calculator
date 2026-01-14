namespace HouseholdBudgetCalculator.Models;

public static class CsvDataTypeMap
{
    public enum CsvFormatType
    {
        PayPay,
        Rakuten
    }

    public static readonly Dictionary<CsvFormatType, Type> TypeMap = new()
    {
        { CsvFormatType.PayPay, typeof(PayPayCsvData) },
        { CsvFormatType.Rakuten, typeof(RakutenCsvData) }
    };
}
