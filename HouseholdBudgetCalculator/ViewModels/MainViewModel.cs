using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.Services;
using HouseholdBudgetCalculator.Views;
using System.Collections.ObjectModel;
using System.Text;

namespace HouseholdBudgetCalculator.ViewModels
{
    public partial class MainViewModel(CsvReaderService csvReaderService, ProductFactory productFactory) : ObservableObject
    {
        private readonly CsvReaderService _csvReaderService = csvReaderService;
        private readonly ProductFactory _productFactory = productFactory;

        [ObservableProperty]
        private ObservableCollection<CsvData> _csvDataList = [];

        [ObservableProperty]
        private ObservableCollection<CategorySummary> _categorySummaries = [];

        [RelayCommand]
        private void LoadCsvFile()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                var data = _csvReaderService.LoadCsv(filePath, Encoding.UTF8);
                CsvDataList = [.. data];
                var products = _productFactory.Create(data);
                AggregateProductsByCategory(products);
            }
        }

        private void AggregateProductsByCategory(List<Product> products)
        {
            var summaries = new List<CategorySummary>();

            // カテゴリ別に集計
            var categoryTotals = products
                .Where(p => p.Category != ProductCategory.Unknown) // Unknownカテゴリを除外
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.TotalPaymentAmount));

            foreach (var category in categoryTotals)
            {
                summaries.Add(new CategorySummary
                {
                    CategoryName = category.Key.ToString(),
                    ProductName = string.Empty, // Unknown以外は空欄
                    TotalAmount = category.Value
                });
            }

            // Unknownカテゴリの個別金額
            var unknownProducts = products.Where(p => p.Category == ProductCategory.Unknown);

            foreach (var product in unknownProducts)
            {
                summaries.Add(new CategorySummary
                {
                    CategoryName = ProductCategory.Unknown.ToString(),
                    ProductName = product.Name.Value,
                    TotalAmount = product.TotalPaymentAmount
                });
            }

            CategorySummaries = [.. summaries];
        }

        [RelayCommand]
        private void ShowCategorySummaryWindow()
        {
            var summaryWindow = new CategorySummaryWindow(new CategorySummaryViewModel(CategorySummaries));
            summaryWindow.Show();
        }
    }
}
