using System.Globalization;
using System.IO;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration;

using HouseholdBudgetCalculator.Models;

namespace HouseholdBudgetCalculator.Services;

public class CsvReaderService
{
    public List<T> LoadCsv<T>(string filePath, Encoding encoding) where T : CsvData
    {
        using var reader = new StreamReader(path: filePath, encoding: encoding);
        using var csv = new CsvReader(reader: reader, configuration: new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            BadDataFound = null,
            MissingFieldFound = null,
        });
        return [.. csv.GetRecords<T>()];
    }
}
