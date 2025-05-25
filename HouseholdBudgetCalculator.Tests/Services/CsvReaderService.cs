using CsvHelper;
using CsvHelper.Configuration;
using HouseholdBudgetCalculator.Models; // This will refer to the copied Models
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic; // For List<T>

// Using original namespace
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
                BadDataFound = null, // Default behavior: throws exception for bad data
                MissingFieldFound = null, // Default behavior: throws exception or sets null/default
            });
            return new List<CsvData>(csv.GetRecords<CsvData>()); // Materialize to list
        }

        // Overload for tests or cases where default UTF-8 is assumed
        public List<CsvData> LoadCsv(string filePath)
        {
            return LoadCsv(filePath, Encoding.UTF8);
        }
    }
}
