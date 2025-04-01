using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.Services;
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
            }
        }
    }
}
