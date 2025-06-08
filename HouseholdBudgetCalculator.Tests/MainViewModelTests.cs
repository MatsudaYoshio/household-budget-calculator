using System; // Added for DateTime
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdBudgetCalculator.Models;
using HouseholdBudgetCalculator.ViewModels;
using HouseholdBudgetCalculator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.IO;

namespace HouseholdBudgetCalculator.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel = null!;
        private ProductFactory _productFactory = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            var productRepository = new ProductRepository();
            _productFactory = new ProductFactory(productRepository);

            var csvReaderService = new CsvReaderService();
            _viewModel = new MainViewModel(csvReaderService, _productFactory);
        }

        private void CallAggregateProductsByCategory(List<Product> products)
        {
            MethodInfo? method = typeof(MainViewModel)
                .GetMethod("AggregateProductsByCategory", BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                throw new System.Exception("Could not find private method AggregateProductsByCategory.");
            }
            method.Invoke(_viewModel, [products]);
        }

        [TestMethod]
        public void AggregateProductsByCategory_UnknownProduct_IncludesDateOfUse()
        {
            // Arrange
            var products = new List<Product>
            {
                new() { Name = new ProductName("Unknown Item 1"), TotalPaymentAmount = 100, Category = ProductCategory.Unknown, DateOfUse = new DateTime(2023, 1, 15) },
                new() { Name = new ProductName("Known Item 1"), TotalPaymentAmount = 200, Category = ProductCategory.Food, DateOfUse = new DateTime(2023, 1, 16) }
            };

            // Act
            CallAggregateProductsByCategory(products);
            var summaries = _viewModel.CategorySummaries;

            // Assert
            var unknownSummary = summaries.FirstOrDefault(s => s.CategoryName == ProductCategory.Unknown.ToString() && s.ProductName == "Unknown Item 1");
            Assert.IsNotNull(unknownSummary, "Unknown summary item not found.");
            Assert.AreEqual(new DateTime(2023, 1, 15), unknownSummary.DateOfUse, "DateOfUse for Unknown item is incorrect.");

            var knownSummary = summaries.FirstOrDefault(s => s.CategoryName == ProductCategory.Food.ToString());
            Assert.IsNotNull(knownSummary, "Known summary item not found.");
            Assert.IsNull(knownSummary.DateOfUse, "DateOfUse for known category item should be null."); // Changed from IsNullOrEmpty
            Assert.IsTrue(string.IsNullOrEmpty(knownSummary.ProductName), "ProductName for known category item should be empty.");
        }

        [TestMethod]
        public void AggregateProductsByCategory_KnownProduct_DateOfUseIsEmpty() // Renaming to DateOfUseIsNull might be more accurate
        {
            // Arrange
            var products = new List<Product>
            {
                new() { Name = new ProductName("Groceries"), TotalPaymentAmount = 500, Category = ProductCategory.Food, DateOfUse = new DateTime(2023, 1, 20) }
            };

            // Act
            CallAggregateProductsByCategory(products);
            var summaries = _viewModel.CategorySummaries;

            // Assert
            var foodSummary = summaries.FirstOrDefault(s => s.CategoryName == ProductCategory.Food.ToString());
            Assert.IsNotNull(foodSummary, "Food summary item not found.");
            Assert.IsNull(foodSummary.DateOfUse, "DateOfUse for Food item should be null."); // Changed from IsNullOrEmpty
            Assert.AreEqual(500, foodSummary.TotalAmount);
        }

        [TestMethod]
        public void AggregateProductsByCategory_MixedProducts_CorrectSummaries()
        {
            // Arrange
            var products = new List<Product>
            {
                new() { Name = new ProductName("Unknown Item A"), TotalPaymentAmount = 10, Category = ProductCategory.Unknown, DateOfUse = new DateTime(2023, 2, 1) },
                new() { Name = new ProductName("Bus Fare"), TotalPaymentAmount = 1200, Category = ProductCategory.Transport, DateOfUse = new DateTime(2023, 2, 5) },
                new() { Name = new ProductName("Unknown Item B"), TotalPaymentAmount = 20, Category = ProductCategory.Unknown, DateOfUse = new DateTime(2023, 2, 10) },
                new() { Name = new ProductName("Dinner Out"), TotalPaymentAmount = 60, Category = ProductCategory.Food, DateOfUse = new DateTime(2023, 2, 15) },
                new() { Name = new ProductName("Lunch"), TotalPaymentAmount = 15, Category = ProductCategory.Food, DateOfUse = new DateTime(2023, 2, 16) },
            };

            // Act
            CallAggregateProductsByCategory(products);
            var summaries = _viewModel.CategorySummaries;

            // Assert
            var unknownA = summaries.FirstOrDefault(s => s.ProductName == "Unknown Item A");
            Assert.IsNotNull(unknownA);
            Assert.AreEqual(ProductCategory.Unknown.ToString(), unknownA.CategoryName);
            Assert.AreEqual(new DateTime(2023, 2, 1), unknownA.DateOfUse);
            Assert.AreEqual(10, unknownA.TotalAmount);

            var unknownB = summaries.FirstOrDefault(s => s.ProductName == "Unknown Item B");
            Assert.IsNotNull(unknownB);
            Assert.AreEqual(ProductCategory.Unknown.ToString(), unknownB.CategoryName);
            Assert.AreEqual(new DateTime(2023, 2, 10), unknownB.DateOfUse);
            Assert.AreEqual(20, unknownB.TotalAmount);

            var transport = summaries.FirstOrDefault(s => s.CategoryName == ProductCategory.Transport.ToString());
            Assert.IsNotNull(transport);
            Assert.IsTrue(string.IsNullOrEmpty(transport.ProductName));
            Assert.IsNull(transport.DateOfUse); // Changed from IsNullOrEmpty
            Assert.AreEqual(1200, transport.TotalAmount);

            var food = summaries.FirstOrDefault(s => s.CategoryName == ProductCategory.Food.ToString());
            Assert.IsNotNull(food);
            Assert.IsTrue(string.IsNullOrEmpty(food.ProductName));
            Assert.IsNull(food.DateOfUse); // Changed from IsNullOrEmpty
            Assert.AreEqual(75, food.TotalAmount); // 60 + 15
        }
    }
}
