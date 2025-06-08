using System; // Added for DateTime
using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Collections.Generic; // Added for List<T>
using System.IO; // Added for Path, Directory, DirectoryNotFoundException
using System.Linq; // Added for ToList()

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
                new() { ProductName = new ProductName("エコバックＳｔａｔｉｏｎ"), TotalPaymentAmount = 1320, DateOfUse = new DateTime(2025, 2, 28) },
                new() { ProductName = new ProductName("東急ストア"), TotalPaymentAmount = 70238, DateOfUse = new DateTime(2025, 2, 28) },
                new() { ProductName = new ProductName("セブンイレブン"), TotalPaymentAmount = 270445, DateOfUse = new DateTime(2025, 2, 26) }
            };

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList();

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match.");
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].ProductName.Value, actualData[i].ProductName.Value, $"ProductName mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].TotalPaymentAmount, actualData[i].TotalPaymentAmount, $"TotalPaymentAmount mismatch at record {i}.");
                Assert.AreEqual(expectedData[i].DateOfUse, actualData[i].DateOfUse, $"DateOfUse mismatch at record {i}.");
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
