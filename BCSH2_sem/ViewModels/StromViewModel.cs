using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

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

        [RelayCommand]
        private void AddStrom()
        {
            var newStrom = new Strom
            {
                // Nastavte výchozí hodnoty
                SpravceID = 1,
                DruhStromu = DruhyStromuTyp.None,
                DatumZasazeni = DateTime.Now,
                DatumPridaniZaznamu = DateTime.Now,
                Lokace = "Nová lokalita", // Příklad
                Vyska = 0.0,              // Příklad
                PrumerKmeni = 0.0,        // Příklad
                TypKury = "Neurčeno"      // Příklad
            };

            // Přidání stromu do databáze a získání jeho ID
            var addedStrom = _sqliteHandler.AddStrom(newStrom);

            // Přiřazení správného ID z databáze
            newStrom.ID = addedStrom.ID;

            // Přidání stromu do ObservableCollection
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
            // Otevřít okno Správci
            new Views.SpravciView().Show();
        }

        [RelayCommand]
        private void ShowRegiony()
        {
            // Otevřít okno Regiony
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
