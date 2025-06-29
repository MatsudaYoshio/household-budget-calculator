using CsvHelper;
using CsvHelper.Configuration;
using HouseholdBudgetCalculator.Models;
using System.Globalization;
using System.IO;
using System.Text;

namespace HouseholdBudgetCalculator.Services
{
    public class CsvReaderService
    {
        public List<CsvData> LoadCsv(string filePath, Encoding encoding, CsvFormatType formatType)
        {
            var formatDefinition = GetCsvFormatDefinition(formatType);
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(filePath, encoding);
            using var csv = new CsvReader(reader, csvConfiguration);

            // ヘッダー行を読み飛ばす
            csv.Read();
            csv.ReadHeader();

            var records = new List<CsvData>();
            while (csv.Read())
            {
                var dateOfUse = csv.GetField<DateOnly?>(formatDefinition.DateOfUseHeader);
                var productNameString = csv.GetField<string>(formatDefinition.ProductNameHeader);
                var productName = new ProductName(productNameString ?? string.Empty, formatDefinition.ProductNamePrefix);
                var totalPaymentAmount = csv.GetField<int>(formatDefinition.TotalPaymentAmountHeader);

                records.Add(new CsvData
                {
                    DateOfUse = dateOfUse,
                    ProductName = productName,
                    TotalPaymentAmount = totalPaymentAmount
                });
            }
            return records;
        }

        private CsvFormatDefinition GetCsvFormatDefinition(CsvFormatType formatType)
        {
            return formatType switch
            {
                CsvFormatType.PayPay => CsvFormatDefinition.PayPayDefinition,
                // 他のCSV形式の定義をここに追加
                _ => throw new ArgumentOutOfRangeException(nameof(formatType), $"Unsupported CSV format: {formatType}"),
            };
        }
    }
}
