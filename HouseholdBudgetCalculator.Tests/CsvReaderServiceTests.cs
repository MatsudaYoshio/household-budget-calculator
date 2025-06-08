using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace HouseholdBudgetCalculator.Tests
{
    [TestClass]
    public class CsvReaderServiceTests
    {
        private string _testDataPath = null!;
        private CsvReaderService _csvReaderService = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            string assemblyPath = Path.GetDirectoryName(typeof(CsvReaderServiceTests).Assembly.Location)!;
            _testDataPath = Path.Combine(assemblyPath, "TestData");
            _csvReaderService = new CsvReaderService();

            if (!Directory.Exists(_testDataPath))
            {
                throw new DirectoryNotFoundException($"TestData directory not found at '{_testDataPath}'. Ensure CSV files are copied to output directory.");
            }
        }

        [TestMethod]
        public void LoadCsv_ValidFile_ReturnsCorrectData()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "valid_data.csv");
            var expectedData = new List<CsvData>
            {
                new() { DateOfUse = new DateOnly(2025, 2, 28), ProductName = new ProductName("ＰａｙＰａｙ　エコバックＳｔａｔｉｏｎ"), TotalPaymentAmount = 1320 },
                new() { DateOfUse = new DateOnly(2025, 12, 31), ProductName = new ProductName("ＰａｙＰａｙ　東急ストア"), TotalPaymentAmount = 70238 },
                new() { DateOfUse = new DateOnly(2025, 1, 1), ProductName = new ProductName("ＰａｙＰａｙ　セブンイレブン"), TotalPaymentAmount = 270445 },
                new() { DateOfUse = new DateOnly(2025, 3, 15), ProductName = new ProductName("ＰａｙＰａｙ　ファミリーマート"), TotalPaymentAmount = 500 },
                new() { DateOfUse = new DateOnly(2025, 4, 10), ProductName = new ProductName("ＰａｙＰａｙ　ローソン"), TotalPaymentAmount = 1200 },
                new() { DateOfUse = new DateOnly(2025, 5, 20), ProductName = new ProductName("ＰａｙＰａｙ　マクドナルド"), TotalPaymentAmount = 850 },
                new() { DateOfUse = new DateOnly(2025, 6, 5), ProductName = new ProductName("ＰａｙＰａｙ　イオン"), TotalPaymentAmount = 3000 }
            };

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList();

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match.");
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].DateOfUse, actualData[i].DateOfUse, $"DateOfUse mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].ProductName.Value, actualData[i].ProductName.Value, $"ProductName mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].TotalPaymentAmount, actualData[i].TotalPaymentAmount, $"TotalPaymentAmount mismatch at record {i}.");
            }
        }

        [TestMethod]
        public void LoadCsv_EmptyFile_ReturnsEmptyList()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "empty_data.csv");

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList();

            // Assert
            Assert.IsNotNull(actualData, "The returned list should not be null.");
            Assert.AreEqual(0, actualData.Count, "The list should be empty for an empty CSV file.");
        }
    }
}
