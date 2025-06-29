namespace HouseholdBudgetCalculator.Services
{
    public enum CsvFormatType
    {
        PayPay,
        // 他のCSV形式をここに追加
    }

    public class CsvFormatDefinition(string dateOfUseHeader, string productNameHeader, string totalPaymentAmountHeader, string? productNamePrefix = null)
    {
        public string DateOfUseHeader { get; } = dateOfUseHeader;
        public string ProductNameHeader { get; } = productNameHeader;
        public string TotalPaymentAmountHeader { get; } = totalPaymentAmountHeader;
        public string? ProductNamePrefix { get; } = productNamePrefix;
    }
}
