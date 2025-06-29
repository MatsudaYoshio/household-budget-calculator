using HouseholdBudgetCalculator.Models;

namespace HouseholdBudgetCalculator.Services
{
    public class ProductFactory(ProductRepository productRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;

        public Product Create(ICsvData csvData)
        {
            var category = _productRepository.Get(csvData.ProductName.Value);

            return new Product
            {
                Name = csvData.ProductName,
                TotalPaymentAmount = csvData.TotalPaymentAmount,
                Category = category == null ? ProductCategory.Unknown : ProductCategoryExtensions.ConvertToEnum(category),
                DateOfUse = csvData.DateOfUse
            };
        }

        public List<Product> Create(List<ICsvData> csvDataList)
        {
            return csvDataList.ConvertAll(Create);
        }
    }
}
