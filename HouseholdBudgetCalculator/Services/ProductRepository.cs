using System.IO;
using System.Text.Json;

namespace HouseholdBudgetCalculator.Services
{
    public class ProductRepository
    {
        private readonly string DATA_PATH = Path.Combine(AppContext.BaseDirectory, "Assets", "Data", "data.json");
        private readonly Dictionary<string, string> _data;

        public ProductRepository()
        {
            var jsonData = File.ReadAllText(DATA_PATH);
            _data = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData) ?? [];
        }

        public string? Get(string key)
        {
            return _data.GetValueOrDefault(key);
        }
    }
}
