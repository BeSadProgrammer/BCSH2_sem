using StromApp.ViewModels;
using System.Windows;

namespace StromApp.Views
{
    public partial class AddRegionDialog : Window
    {
        public AddRegionDialog()
        {
            InitializeComponent();
            this.DataContext = new AddRegionViewModel();  // Přiřazení ViewModelu jako DataContext
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Tlačítko pro přidání regionu
            var viewModel = (AddRegionViewModel)DataContext;
            viewModel.AddRegion();
            this.Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            // Tlačítko pro zrušení
            this.Close();
        }
    }
}
