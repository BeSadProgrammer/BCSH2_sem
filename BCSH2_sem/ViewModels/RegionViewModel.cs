using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

public partial class RegionViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Region> regions;

    [ObservableProperty]
    private Region selectedRegion; // Selected region property

    public ICommand NewRegionCommand { get; }
    public ICommand EditRegionCommand { get; }
    public ICommand DeleteRegionCommand { get; }
    public ICommand CloseCommand { get; }

    public RegionViewModel()
    {
        // Initialize the region list from the database
        Regions = new ObservableCollection<Region>(SQLiteHandler.GetRegions());

        // Commands for actions
        NewRegionCommand = new RelayCommand(OnNewRegion);
        EditRegionCommand = new RelayCommand(OnEditRegion);
        DeleteRegionCommand = new RelayCommand(OnDeleteRegion);
        CloseCommand = new RelayCommand(OnCloseRegionView);
    }

    // Method for adding a new region
    private void OnNewRegion()
    {
        var newRegion = new Region("New Region");
        SQLiteHandler.InsertRegion(newRegion); // Add to database
        Regions.Add(newRegion); // Add to ObservableCollection
    }

    // Method for editing a selected region
    private void OnEditRegion()
    {
        if (SelectedRegion != null)
        {
            // Logic for editing the selected region
            // This could involve opening a new dialog or directly modifying the properties
        }
    }

    // Method for deleting a selected region
    private void OnDeleteRegion()
    {
        if (SelectedRegion != null)
        {
            SQLiteHandler.DeleteRegion(SelectedRegion.RegionID); // Delete from database
            Regions.Remove(SelectedRegion); // Remove from ObservableCollection
        }
    }

    // Close the RegionView window
    private void OnCloseRegionView()
    {
        Application.Current.Windows.OfType<RegionView>().FirstOrDefault()?.Close();
    }
}
