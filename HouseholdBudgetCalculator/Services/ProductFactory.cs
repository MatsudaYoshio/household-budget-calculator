using HouseholdBudgetCalculator.Models;
using System.Collections.Generic;

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

        // 既存の List<CsvData> を受け取るメソッドも残しておく場合（あるいは削除してICsvDataに統一する）
        public List<Product> Create(List<CsvData> csvDataList)
        {
            // ICsvDataのリストにキャストして共通処理を呼び出す
            return csvDataList.ConvertAll(item => Create((ICsvData)item));
        }
    }
}
