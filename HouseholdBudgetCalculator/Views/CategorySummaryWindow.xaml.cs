using HouseholdBudgetCalculator.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace HouseholdBudgetCalculator.Views
{
    public partial class CategorySummaryWindow : Window
    {
        public CategorySummaryWindow(CategorySummaryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CopyToClipboard(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is not null)
            {
                Clipboard.SetText(element.Tag.ToString());
            }
        }
    }
}
