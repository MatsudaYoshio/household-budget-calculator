using HouseholdBudgetCalculator.Models;
using System.Text;
using static HouseholdBudgetCalculator.Models.CsvDataTypeMap;

namespace HouseholdBudgetCalculator.Services
{
    public class GenericCsvDataLoader(CsvReaderService csvReaderService)
    {
        private readonly CsvReaderService _csvReaderService = csvReaderService;

        public List<CsvData> Load(CsvFormatType formatType, string filePath, Encoding encoding)
        {
            if (!TypeMap.TryGetValue(formatType, out var type))
                throw new ArgumentException($"Unsupported format: {formatType}");

            var method = typeof(CsvReaderService).GetMethod(nameof(CsvReaderService.LoadCsv)) ?? throw new MissingMethodException($"Method '{nameof(CsvReaderService.LoadCsv)}' not found in '{typeof(CsvReaderService).Name}'.");
            method = method.MakeGenericMethod(type);
            var result = method.Invoke(_csvReaderService, [filePath, encoding]);
            if (result is System.Collections.IEnumerable enumerable)
            {
                return [.. enumerable.Cast<CsvData>()];
            }
            throw new InvalidCastException("The loaded CSV data could not be cast to List<CsvData>.");
        }
    }
}
