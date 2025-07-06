using CsvHelper.Configuration.Attributes;
using HouseholdBudgetCalculator.Models.Conveters;

namespace HouseholdBudgetCalculator.Models
{
    public class PayPayCsvData : CsvData
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
}
