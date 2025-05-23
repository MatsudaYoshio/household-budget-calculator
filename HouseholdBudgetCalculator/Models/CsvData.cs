﻿using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace HouseholdBudgetCalculator.Models
{
    public class CsvData
    {
        [Name("利用店名・商品名")]
        [TypeConverter(typeof(ProductNameConverter))]
        public ProductName ProductName { get; set; } = null!;
        [Name("支払総額")]
        public int TotalPaymentAmount { get; set; }
    }

    public class ProductNameConverter : ITypeConverter
    {
        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text is not null ? new ProductName(text) : null;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return value is ProductName productName ? productName.ToString() : string.Empty;
        }
    }
}
