using CsvHelper.Configuration.Attributes;

namespace HouseholdBudgetCalculator.Models
{
    internal interface ICsvData
    {
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly? DateOfUse { get; set; }
        public ProductName ProductName { get; set; }
        public int TotalPaymentAmount { get; set; }
    }
}
