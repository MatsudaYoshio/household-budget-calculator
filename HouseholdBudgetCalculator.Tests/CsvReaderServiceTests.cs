using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdBudgetCalculator.Services; // Resolves to local copy
using HouseholdBudgetCalculator.Models;   // Resolves to local copy
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text; // For Encoding
using CsvHelper; // General CsvHelper namespace
using CsvHelper.TypeConversion; // For TypeConverterException

namespace HouseholdBudgetCalculator.Tests
{
    [TestClass]
    public class CsvReaderServiceTests
    {
        private string _testDataPath;
        private CsvReaderService _csvReaderService;

        [TestInitialize]
        public void TestInitialize()
        {
            string assemblyPath = Path.GetDirectoryName(typeof(CsvReaderServiceTests).Assembly.Location);
            _testDataPath = Path.Combine(assemblyPath, "TestData");
            _csvReaderService = new CsvReaderService(); // Uses the copied service

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
                new CsvData { ProductName = new ProductName("Test Product 1"), TotalPaymentAmount = 100 },
                new CsvData { ProductName = new ProductName("Test Product 2"), TotalPaymentAmount = 200 }
            };

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList();

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match.");
            for (int i = 0; i < expectedData.Count; i++)
            {
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

        [TestMethod]
        public void LoadCsv_DifferentEncoding_ReturnsCorrectData()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "utf16_data.csv"); // This file was saved as UTF-16 LE
            var expectedData = new List<CsvData>
            {
                new CsvData { ProductName = new ProductName("UTF16 Product A"), TotalPaymentAmount = 500 },
                new CsvData { ProductName = new ProductName("UTF16 Product B"), TotalPaymentAmount = 600 }
            };
            
            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.Unicode).ToList(); // Encoding.Unicode is for UTF-16

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match for UTF-16 file.");
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].ProductName.Value, actualData[i].ProductName.Value, $"ProductName mismatch at record {i} for UTF-16 file.");
                Assert.AreEqual(expectedData[i].TotalPaymentAmount, actualData[i].TotalPaymentAmount, $"TotalPaymentAmount mismatch at record {i} for UTF-16 file.");
            }
        }

        [TestMethod]
        public void LoadCsv_FileWithMissingFields_ThrowsTypeConverterExceptionForEmptyIntField()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "missing_fields.csv");
            // missing_fields.csv:
            // "利用店名・商品名","支払総額"
            // "Test Product 1",  <-- Empty string for TotalPaymentAmount (int)
            // ,300                <-- Empty string for ProductName
            // "ＰａｙＰａｙ　Test Product",150

            // CsvReaderService uses BadDataFound = null, which means CsvHelper throws an exception for conversion errors.
            // The first record ("Test Product 1",) will cause a TypeConverterException for TotalPaymentAmount.

            // Act & Assert
            var ex = Assert.ThrowsException<CsvHelper.TypeConversion.TypeConverterException>(() => // Fully qualified name
            {
                _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList(); // Materialize the list to trigger parsing
            });

            // Optionally, assert more details about the exception
            Assert.IsNotNull(ex, "A TypeConverterException should be thrown.");
            Assert.IsTrue(ex.Message.Contains("The conversion cannot be performed."), "Exception message should indicate conversion failure.");
            Assert.IsTrue(ex.Message.Contains("Text: ''"), "Exception message should show the problematic text was an empty string for the integer field.");
            Assert.IsTrue(ex.Message.Contains("MemberType: System.Int32"), "Exception message should specify the target type was Int32.");
        }

        [TestMethod]
        public void LoadCsv_FileWithExtraFields_HandlesGracefully()
        {
            // Arrange
            string filePath = Path.Combine(_testDataPath, "extra_fields.csv");
            // extra_fields.csv:
            // "利用店名・商品名","支払総額","Extra Column"
            // "Test Product 1",100,"ExtraVal1"
            // "Test Product 2",200,"ExtraVal2"
            // CsvHelper should ignore "Extra Column" by default.
            var expectedData = new List<CsvData>
            {
                new CsvData { ProductName = new ProductName("Test Product 1"), TotalPaymentAmount = 100 },
                new CsvData { ProductName = new ProductName("Test Product 2"), TotalPaymentAmount = 200 }
            };

            // Act
            var actualData = _csvReaderService.LoadCsv(filePath, Encoding.UTF8).ToList();

            // Assert
            Assert.AreEqual(expectedData.Count, actualData.Count, "Number of records should match for extra fields file.");
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].ProductName.Value, actualData[i].ProductName.Value, $"ProductName mismatch at record {i} for extra fields file.");
                Assert.AreEqual(expectedData[i].TotalPaymentAmount, actualData[i].TotalPaymentAmount, $"TotalPaymentAmount mismatch at record {i} for extra fields file.");
            }
        }
    }
}
