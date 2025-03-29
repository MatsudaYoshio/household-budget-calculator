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
        public List<CsvData> LoadCsv(string filePath, Encoding encoding)
        {
            using var reader = new StreamReader(filePath, encoding);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null,
            });
            return [.. csv.GetRecords<CsvData>()];
        }
    }
}
