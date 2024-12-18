using BCSH2_sem;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;

namespace StromApp.ViewModels
{
    public partial class EditRegionViewModel : ObservableObject
    {
        private Region _region;

        [ObservableProperty]
        private string regionName;

        public EditRegionViewModel(Region selectedRegion)
        {
            _region = selectedRegion;
            RegionName = selectedRegion.NazevRegionu;
        }

        [RelayCommand]
        private void EditRegion()
        {
            if (string.IsNullOrWhiteSpace(RegionName))
            {
                // Handle error: region name is required
                return;
            }

            _region.NazevRegionu = RegionName;

            // Update the region in the database
            var handler = new SQLiteHandler();
            handler.UpdateRegion(_region);

            // Close the dialog
            var dialog = (EditRegionDialog)App.Current.Windows.OfType<EditRegionDialog>().FirstOrDefault();
            dialog?.Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            var dialog = (EditRegionDialog)App.Current.Windows.OfType<EditRegionDialog>().FirstOrDefault();
            dialog?.Close();
        }
    }
}