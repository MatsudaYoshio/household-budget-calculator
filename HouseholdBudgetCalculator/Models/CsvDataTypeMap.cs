namespace HouseholdBudgetCalculator.Models;

public static class CsvDataTypeMap
{
    public enum CsvFormatType
    {
        PayPay
    }

    public static readonly Dictionary<CsvFormatType, Type> TypeMap = new()
    {
        { CsvFormatType.PayPay, typeof(PayPayCsvData) },
    };
}
