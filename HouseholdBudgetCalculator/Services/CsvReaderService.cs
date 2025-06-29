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
        public List<ICsvData> LoadCsv(string filePath, Encoding encoding, CsvFormatType formatType)
        {
            ICsvData csvDataTemplate = CreateCsvDataTemplate(formatType);
            var formatDefinition = csvDataTemplate.FormatDefinition;

            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null,
            };

            using var reader = new StreamReader(filePath, encoding);
            using var csv = new CsvReader(reader, csvConfiguration);

            csv.Read();
            csv.ReadHeader();

            // ファイルが空、またはヘッダ行がない場合は空リストを返す
            if (!csv.Read() || !csv.ReadHeader()) { return []; }

            var records = new List<ICsvData>();
            while (csv.Read())
            {
                var dateOfUse = csv.GetField<DateOnly?>(formatDefinition.DateOfUseHeader);
                var productNameString = csv.GetField<string>(formatDefinition.ProductNameHeader);
                var productName = new ProductName(productNameString ?? string.Empty, formatDefinition.ProductNamePrefix);
                var totalPaymentAmount = csv.GetField<int>(formatDefinition.TotalPaymentAmountHeader);

                // インスタンスの生成方法を改善する必要があるかもしれない
                ICsvData recordInstance = CreateCsvDataTemplate(formatType);
                recordInstance.DateOfUse = dateOfUse;
                recordInstance.ProductName = productName;
                recordInstance.TotalPaymentAmount = totalPaymentAmount;
                records.Add(recordInstance);
            }
            return records;
        }

        private static ICsvData CreateCsvDataTemplate(CsvFormatType formatType)
        {
            return formatType switch
            {
                CsvFormatType.PayPay => new PayPayCsvData(),
                // 他のCSV形式のインスタンス生成をここに追加
                _ => throw new ArgumentOutOfRangeException(nameof(formatType), $"Unsupported CSV format: {formatType}"),
            };
        }
    }
}
