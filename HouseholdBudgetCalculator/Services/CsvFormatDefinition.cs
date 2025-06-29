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

        // Paypayの定義例
        public static CsvFormatDefinition PayPayDefinition =>
            new("利用日/キャンセル日", "利用店名・商品名", "支払総額", "ＰａｙＰａｙ　");
    }
}
