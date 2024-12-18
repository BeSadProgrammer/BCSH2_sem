using BCSH2_sem;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.ViewModels;
using StromApp.Views;

public partial class EditRegionViewModel : ObservableObject
{
    private Region _region;
    private SQLiteHandler _sqliteHandler;
    private RegionyViewModel _regionyViewModel;

    [ObservableProperty]
    private string regionName;

    public EditRegionViewModel(Region selectedRegion, RegionyViewModel regionyViewModel)
    {
        _sqliteHandler = new SQLiteHandler();
        _regionyViewModel = regionyViewModel;
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
        _sqliteHandler.UpdateRegion(_region);

        // Reload all regions to ensure data is updated
        _regionyViewModel.Regiony.Clear();
        var updatedRegiony = _sqliteHandler.GetAllRegiony();
        foreach (var region in updatedRegiony)
        {
            _regionyViewModel.Regiony.Add(region);
        }

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
