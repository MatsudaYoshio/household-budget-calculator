using System.Collections.ObjectModel;

namespace HouseholdBudgetCalculator.ViewModels
{
    public class CategorySummaryViewModel(ObservableCollection<CategorySummary> categorySummaries)
    {
        public ObservableCollection<CategorySummary> CategorySummaries { get; } = categorySummaries;
    }
}