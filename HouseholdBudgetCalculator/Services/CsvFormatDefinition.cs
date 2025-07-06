namespace HouseholdBudgetCalculator.Services
{
    public enum CsvFormatType
    {
        PayPay
    }

    public class CsvFormatDefinition(string dateOfUseHeader, string productNameHeader, string totalPaymentAmountHeader, string? productNamePrefix = null)
    {
        public string DateOfUseHeader { get; } = dateOfUseHeader;
        public string ProductNameHeader { get; } = productNameHeader;
        public string TotalPaymentAmountHeader { get; } = totalPaymentAmountHeader;
        public string? ProductNamePrefix { get; } = productNamePrefix;
    }
}
