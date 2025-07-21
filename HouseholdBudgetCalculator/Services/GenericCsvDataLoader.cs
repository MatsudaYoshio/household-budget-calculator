using System.Reflection;
using System.Text;

using HouseholdBudgetCalculator.Models;

using static HouseholdBudgetCalculator.Models.CsvDataTypeMap;
namespace HouseholdBudgetCalculator.Services;

public class GenericCsvDataLoader
{
    private readonly CsvReaderService _csvReaderService;
    private readonly Dictionary<CsvFormatType, Func<string, Encoding, List<CsvData>>> _loadFunctions;
    public GenericCsvDataLoader(CsvReaderService csvReaderService)
    {
        _csvReaderService = csvReaderService;
        _loadFunctions = [];

        var loadCsvMethodInfo = typeof(CsvReaderService)
            .GetMethod(
                name: nameof(CsvReaderService.LoadCsv),
                bindingAttr: BindingFlags.Public | BindingFlags.Instance,
                binder: null,
                types: [typeof(string), typeof(Encoding)],
                modifiers: null
            )
            ?? throw new MissingMethodException($"Method '{nameof(CsvReaderService.LoadCsv)}' not found in '{typeof(CsvReaderService).Name}'.");


        foreach (var (csvFormatType, dataType) in TypeMap)
        {
            var specificLoadCsvMethod = loadCsvMethodInfo.MakeGenericMethod(dataType);
            var loadSpecificTypeFunc = (Func<string, Encoding, object>) Delegate.CreateDelegate(
                type: typeof(Func<string, Encoding, object>),
                firstArgument: _csvReaderService,
                method: specificLoadCsvMethod
            );

            _loadFunctions.Add(key: csvFormatType, value: (filePath, encoding) =>
            {
                var result = loadSpecificTypeFunc(filePath, encoding);
                if (result is System.Collections.IEnumerable enumerable)
                {
                    return [.. enumerable.Cast<CsvData>()];
                }
                throw new InvalidCastException($"The loaded CSV data of type {dataType.Name} could not be cast to List<CsvData>.");
            });
        }
    }
    public List<CsvData> Load(CsvFormatType formatType, string filePath, Encoding encoding)
    {
        if (_loadFunctions.TryGetValue(key: formatType, value: out var loadFunction))
        {
            return loadFunction(filePath, encoding);
        }
        throw new ArgumentException($"Unsupported format: {formatType}. Available formats: {string.Join(", ", _loadFunctions.Keys)}");
    }
}
