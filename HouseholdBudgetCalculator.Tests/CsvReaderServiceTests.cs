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
        public void LoadCsv_ValidFile_PayPayFormat_ReturnsCorrectData()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "valid_data.csv");
            var expectedData = new List<ICsvData> // 型を ICsvData に変更
            {
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 2, 28), ProductName = new ProductName("エコバックＳｔａｔｉｏｎ", "ＰａｙＰａｙ　"), TotalPaymentAmount = 1320 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 12, 31), ProductName = new ProductName("東急ストア", "ＰａｙＰａｙ　"), TotalPaymentAmount = 70238 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 1, 1), ProductName = new ProductName("セブンイレブン", "ＰａｙＰａｙ　"), TotalPaymentAmount = 270445 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 3, 15), ProductName = new ProductName("ファミリーマート", "ＰａｙＰａｙ　"), TotalPaymentAmount = 500 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 4, 10), ProductName = new ProductName("ローソン", "ＰａｙＰａｙ　"), TotalPaymentAmount = 1200 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 5, 20), ProductName = new ProductName("マクドナルド", "ＰａｙＰａｙ　"), TotalPaymentAmount = 850 },
                new PayPayCsvData { DateOfUse = new DateOnly(2025, 6, 5), ProductName = new ProductName("イオン", "ＰａｙＰａｙ　"), TotalPaymentAmount = 3000 }
            };

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8, CsvFormatType.PayPay).ToList();

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match.");
            for (int i = 0; i < expectedData.Count; i++)
            {
                // 実際のオブジェクトの型も確認 (オプション)
                Assert.IsInstanceOfType(actualData[i], typeof(PayPayCsvData), $"Record {i} is not of type PayPayCsvData.");
                Assert.AreEqual(expectedData[i].DateOfUse, actualData[i].DateOfUse, $"DateOfUse mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].ProductName.Value, actualData[i].ProductName.Value, $"ProductName mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].TotalPaymentAmount, actualData[i].TotalPaymentAmount, $"TotalPaymentAmount mismatch at record {i}.");
            }
        }
    }
}
