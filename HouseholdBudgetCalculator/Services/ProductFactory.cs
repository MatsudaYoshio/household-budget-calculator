using HouseholdBudgetCalculator.Models;

namespace HouseholdBudgetCalculator.Services
{
    public class ProductFactory(ProductRepository productRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;

        public Product Create(CsvData csvData)
        {
            var category = _productRepository.Get(csvData.ProductName.Value);

            return new Product
            {
                Name = csvData.ProductName,
                TotalPaymentAmount = csvData.TotalPaymentAmount,
                Category = category == null ? ProductCategory.Unknown : ProductCategoryExtensions.ConvertToEnum(category)
            };
        }

        public List<Product> Create(List<CsvData> csvDataList)
        {
            return csvDataList.ConvertAll(Create);
        }
    }
}
