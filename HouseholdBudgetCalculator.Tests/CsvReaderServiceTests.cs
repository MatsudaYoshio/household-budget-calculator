using System.Text;

using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HouseholdBudgetCalculator.Tests;

[TestClass]
public class CsvReaderServiceTests
{
    private string _testDataPath = null!;
    private CsvReaderService _csvReaderService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var assemblyPath = Path.GetDirectoryName(typeof(CsvReaderServiceTests).Assembly.Location)!;
        _testDataPath = Path.Combine(assemblyPath, "TestData");
        _csvReaderService = new CsvReaderService();

        if (!Directory.Exists(_testDataPath))
        {
            throw new DirectoryNotFoundException($"TestData directory not found at '{_testDataPath}'. Ensure CSV files are copied to output directory.");
        }
    }

    [TestMethod]
    public void LoadCsv_ValidFile_PayPayFormat_ReturnsCorrectData()
    {
        // Arrange
        var filePath = Path.Combine(_testDataPath, "valid_data.csv");
        var expectedData = new List<CsvData>
        {
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 2, 28), ProductName = new ProductName("エコバックＳｔａｔｉｏｎ"), TotalPaymentAmount = 1320 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 12, 31), ProductName = new ProductName("東急ストア"), TotalPaymentAmount = 70238 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 1, 1), ProductName = new ProductName("セブンイレブン"), TotalPaymentAmount = 270445 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 3, 15), ProductName = new ProductName("ファミリーマート"), TotalPaymentAmount = 500 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 4, 10), ProductName = new ProductName("ローソン"), TotalPaymentAmount = 1200 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 5, 20), ProductName = new ProductName("マクドナルド"), TotalPaymentAmount = 850 },
            new PayPayCsvData { DateOfUse = new DateOnly(2025, 6, 5), ProductName = new ProductName("イオン"), TotalPaymentAmount = 3000 }
        };

        // Act
        var actualData = _csvReaderService.LoadCsv<PayPayCsvData>(filePath, Encoding.UTF8).ToList();

        // Assert
        Assert.AreEqual(expected: expectedData.Count, actual: actualData.Count, message: "Number of records should match.");
        for (var i = 0; i < expectedData.Count; i++)
        {
            // 実際のオブジェクトの型も確認 (オプション)
            Assert.IsInstanceOfType(value: actualData[i], expectedType: typeof(PayPayCsvData), message: $"Record {i} is not of type PayPayCsvData.");
            Assert.AreEqual(expected: expectedData[i].DateOfUse, actual: actualData[i].DateOfUse, message: $"DateOfUse mismatch at record {i}.");
            Assert.AreEqual(expected: expectedData[i].ProductName.Value, actual: actualData[i].ProductName.Value, message: $"ProductName mismatch at record {i}.");
            Assert.AreEqual(expected: expectedData[i].TotalPaymentAmount, actual: actualData[i].TotalPaymentAmount, message: $"TotalPaymentAmount mismatch at record {i}.");
        }
    }
}
