namespace HouseholdBudgetCalculator.Services
{
    public enum CsvFormatType
    {
        PayPay,
        // 他のCSV形式をここに追加
    }

    public class CsvFormatDefinition
    {
        public string DateOfUseHeader { get; }
        public string ProductNameHeader { get; }
        public string TotalPaymentAmountHeader { get; }
        public string? ProductNamePrefix { get; }

        public CsvFormatDefinition(string dateOfUseHeader, string productNameHeader, string totalPaymentAmountHeader, string? productNamePrefix = null)
        {
            DateOfUseHeader = dateOfUseHeader;
            ProductNameHeader = productNameHeader;
            TotalPaymentAmountHeader = totalPaymentAmountHeader;
            ProductNamePrefix = productNamePrefix;
        }
    }
}
