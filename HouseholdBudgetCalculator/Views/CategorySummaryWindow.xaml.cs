using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

using HouseholdBudgetCalculator.ViewModels;

namespace HouseholdBudgetCalculator.Views
{
    public partial class CategorySummaryWindow: Window
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
                try
                {
                    Dispatcher.Invoke(() => Clipboard.SetText(element.Tag.ToString()));
                }
                catch (COMException)
                {
                    MessageBox.Show("Failed to access the clipboard. Please try again.");
                }
            }
        }
    }
}
