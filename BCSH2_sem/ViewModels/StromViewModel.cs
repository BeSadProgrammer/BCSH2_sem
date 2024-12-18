using System.Collections.ObjectModel;
using StromApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System;

namespace StromApp.ViewModels
{
    public partial class StromViewModel : ObservableObject
    {
        // Kolekce stromů pro binding
        [ObservableProperty]
        private ObservableCollection<Strom> stromy;

        // Vybraný strom pro binding
        [ObservableProperty]
        private Strom selectedStrom;

        // Příkaz pro přidání nového stromu
        public ICommand NewStromCommand { get; }

        // Příkaz pro úpravu stromu
        public ICommand EditStromCommand { get; }

        // Příkaz pro smazání vybraného stromu
        public ICommand DeleteSelectedStromCommand { get; }

        // Příkaz pro smazání všech stromů
        public ICommand DeleteAllStromyCommand { get; }

        // Filtrace stromů
        [ObservableProperty]
        private string searchQuery;

        // Konstruktor
        public StromViewModel()
        {
            Stromy = new ObservableCollection<Strom>(LoadStromy()); // Load stromy from a method or service
            NewStromCommand = new RelayCommand(OnNewStrom);
            EditStromCommand = new RelayCommand(OnEditStrom);
            DeleteSelectedStromCommand = new RelayCommand(OnDeleteSelectedStrom);
            DeleteAllStromyCommand = new RelayCommand(OnDeleteAllStromy);
        }

        // Simulating loading trees (replace with real data access logic)
        private IEnumerable<Strom> LoadStromy()
        {
            // This should be replaced with actual data loading logic (e.g., database or repository)
            return new List<Strom>(); // Empty for now, replace with actual list of trees
        }

        // Metody pro příkazy
        private void OnNewStrom()
        {
            // Logika pro přidání nového stromu
            // Open a new dialog or form to add a new tree to the collection
            var newTree = new Strom(DruhyStromuTyp.None, null, DateTime.Now, DateTime.Now, "New Location", 10, 30, "Bark Type");
            Stromy.Add(newTree);
        }

        private void OnEditStrom()
        {
            // Logika pro úpravu existujícího stromu
            // Here, you should implement logic to edit a selected tree, likely by opening an edit form.
        }

        private void OnDeleteSelectedStrom()
        {
            // Pokud je vybraný strom, odstraňte ho z kolekce
            if (SelectedStrom != null)
            {
                Stromy.Remove(SelectedStrom);
                SelectedStrom = null; // Volitelné: zrušení výběru stromu po smazání
            }
        }

        private void OnDeleteAllStromy()
        {
            // Logika pro smazání všech stromů
            Stromy.Clear();
        }

        // Filtrace stromů na základě dotazu
        public void FilterStromy()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                Stromy = new ObservableCollection<Strom>(LoadStromy()); // Reload all if no search query
            }
            else
            {
                var filtered = LoadStromy()
                    .Where(strom => strom.Lokace.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                    strom.StromTyp.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Stromy = new ObservableCollection<Strom>(filtered);
            }
        }
    }
}
