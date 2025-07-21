using System.Collections.ObjectModel;

using HouseholdBudgetCalculator.Models;

namespace HouseholdBudgetCalculator.ViewModels
{
    public class CategorySummaryViewModel(ObservableCollection<CategorySummary> categorySummaries)
    {
        public ObservableCollection<CategorySummary> CategorySummaries { get; } = categorySummaries;
    }
}
