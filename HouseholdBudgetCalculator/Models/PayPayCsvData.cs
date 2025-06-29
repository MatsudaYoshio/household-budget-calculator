using CsvHelper.Configuration.Attributes;
using HouseholdBudgetCalculator.Services; // CsvFormatDefinition を使うため

namespace HouseholdBudgetCalculator.Models
{
    public class PayPayCsvData : ICsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly? DateOfUse { get; set; }
        public ProductName ProductName { get; set; } = null!;
        public int TotalPaymentAmount { get; set; }
        public CsvFormatDefinition FormatDefinition { get; }

        public PayPayCsvData()
        {
            FormatDefinition = new CsvFormatDefinition(
                "利用日/キャンセル日",
                "利用店名・商品名",
                "支払総額",
                "ＰａｙＰａｙ　"
            );
        }
    }
}
