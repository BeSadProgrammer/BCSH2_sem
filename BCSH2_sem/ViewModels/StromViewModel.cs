using System.Collections.ObjectModel;
using StromApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System;
using StromApp.Handlers;
using StromApp.Views;

namespace StromApp.ViewModels
{
    public partial class StromViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Strom> stromy;

        [ObservableProperty]
        private Strom selectedStrom;

        public ICommand NewStromCommand { get; }
        public ICommand EditStromCommand { get; }
        public ICommand DeleteSelectedStromCommand { get; }
        public ICommand DeleteAllStromyCommand { get; }
        public ICommand RegionCommand { get; }

        [ObservableProperty]
        private string searchQuery;

        public StromViewModel()
        {
            SQLiteHandler.InitializeDatabase(); // Ensure DB is initialized
            Stromy = new ObservableCollection<Strom>(SQLiteHandler.GetStromy()); // Load trees from SQLite
            NewStromCommand = new RelayCommand(OnNewStrom);
            EditStromCommand = new RelayCommand(OnEditStrom);
            DeleteSelectedStromCommand = new RelayCommand(OnDeleteSelectedStrom);
            DeleteAllStromyCommand = new RelayCommand(OnDeleteAllStromy);
            RegionCommand = new RelayCommand(OpenRegionView); // Initialize the command

        }

        // Method for adding a new tree
        private void OnNewStrom()
        {
            var newTree = new Strom(DruhyStromuTyp.None, new Spravce("x", "y", "z", "+4", new Region("testss")), DateTime.Now, DateTime.Now, "New Location", 10, 30, "Bark Type");
            SQLiteHandler.InsertStrom(newTree); // Insert the new tree into SQLite
            Stromy.Add(newTree); // Add to ObservableCollection
        }

        private void OnEditStrom()
        {
            // Logic to edit an existing tree. This would typically involve opening a dialog to edit the tree details.
        }

        private void OnDeleteSelectedStrom()
        {
            if (SelectedStrom != null)
            {
                SQLiteHandler.DeleteStrom(SelectedStrom.ID); // Delete from SQLite
                Stromy.Remove(SelectedStrom); // Remove from collection
                SelectedStrom = null; // Optional: clear the selected tree
            }
        }

        private void OnDeleteAllStromy()
        {
            foreach (var strom in Stromy.ToList())
            {
                SQLiteHandler.DeleteStrom(strom.ID); // Delete each tree from SQLite
            }
            Stromy.Clear(); // Clear ObservableCollection
        }

        public void FilterStromy()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                Stromy = new ObservableCollection<Strom>(SQLiteHandler.GetStromy()); // Reload all trees
            }
            else
            {
                var filtered = SQLiteHandler.GetStromy()
                    .Where(strom => strom.Lokace.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                    strom.StromTyp.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Stromy = new ObservableCollection<Strom>(filtered);
            }
        }

        private void OpenRegionView()
        {
            // Create and show the RegionView window
            RegionView regionView = new RegionView();
            regionView.ShowDialog(); // Display the RegionView window
        }
    }
}
