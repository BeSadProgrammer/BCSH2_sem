using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace StromApp.ViewModels
{
    public partial class StromViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Strom> stromy;

        [ObservableProperty]
        private Strom? selectedStrom;

        [ObservableProperty]
        private string? searchText;

        private SQLiteHandler _sqliteHandler;

        public StromViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
            Stromy = new ObservableCollection<Strom>();
            LoadData();
        }

        private void LoadData()
        {
            // Load data from SQLiteHandler
            Stromy = new ObservableCollection<Strom>(_sqliteHandler.GetAllStromy());
        }

        // Command to show AddStromDialog
        [RelayCommand]
        private void ShowAddStromDialog()
        {
            var addStromDialog = new AddStromDialog
            {
                DataContext = new AddStromViewModel() // Assign the ViewModel for the AddStromDialog
            };

            addStromDialog.ShowDialog();
        }

        [RelayCommand]
        private void AddStrom(AddStromViewModel addStromViewModel)
        {
            // Retrieve the values from AddStromViewModel
            var newStrom = new Strom
            {
                SpravceID = addStromViewModel.SelectedSpravce.ID,
                DruhStromu = addStromViewModel.SelectedDruhStromu,
                DatumZasazeni = addStromViewModel.DatumZasazeni ?? DateTime.Now,
                DatumPridaniZaznamu = DateTime.Now,
                Lokace = addStromViewModel.Lokace.Trim(),
                Vyska = addStromViewModel.Vyska,
                PrumerKmeni = addStromViewModel.PrumerKmeni,
                TypKury = addStromViewModel.TypKury.Trim()
            };

            // Validate the input data (optional)
            if (string.IsNullOrEmpty(newStrom.Lokace) || newStrom.Vyska <= 0 || newStrom.PrumerKmeni <= 0)
            {
                // Show error or validation message
                return;
            }

            // Add the tree to the database
            var addedStrom = _sqliteHandler.AddStrom(newStrom);

            // Update the ID of the new tree
            newStrom.ID = addedStrom.ID;

            // Add the new tree to the ObservableCollection
            Stromy.Add(newStrom);
        }

        [RelayCommand]
        private void DeleteStrom()
        {
            if (SelectedStrom == null) return;
            _sqliteHandler.DeleteStrom(SelectedStrom.ID);
            Stromy.Remove(SelectedStrom);
            SelectedStrom = Stromy.FirstOrDefault();
        }

        [RelayCommand]
        private void ShowSpravci()
        {
            // Open Správci view
            new Views.SpravciView().Show();
        }

        [RelayCommand]
        private void ShowRegiony()
        {
            // Open Regiony view
            new Views.RegionyView().Show();
        }

        [RelayCommand]
        private void ClearStromy()
        {
            _sqliteHandler.ClearStromy();
            Stromy.Clear();
        }

        [RelayCommand]
        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadData();
                return;
            }

            var filtered = _sqliteHandler.SearchStromy(SearchText);
            Stromy = new ObservableCollection<Strom>(filtered);
        }
    }
}
