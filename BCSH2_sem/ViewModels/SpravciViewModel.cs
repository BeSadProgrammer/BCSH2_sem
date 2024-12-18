using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class SpravciViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Spravce> spravci;

        [ObservableProperty]
        private Spravce? selectedSpravce;

        private SQLiteHandler _sqliteHandler;

        public SpravciViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
            Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());
        }

        [RelayCommand]
        private void AddSpravce()
        {
            var newSpravce = new Spravce
            {
                Jmeno = "Nový",
                Prijmeni = "Správce",
                Email = "",
                Telefon = "",
                RegionID = 1
            };

            // Přidání správce do databáze a získání jeho ID
            var addedSpravce = _sqliteHandler.AddSpravce(newSpravce);

            // Přiřazení správného ID z databáze
            newSpravce.ID = addedSpravce.ID;

            // Přidání správce do ObservableCollection
            Spravci.Add(newSpravce);
        }

        [RelayCommand]
        private void DeleteSpravce()
        {
            if (SelectedSpravce == null) return;
            _sqliteHandler.DeleteSpravce(SelectedSpravce.ID);
            Spravci.Remove(SelectedSpravce);
            SelectedSpravce = Spravci.FirstOrDefault();
        }

        [RelayCommand]
        private void CloseWindow()
        {
            System.Windows.Application.Current.Windows
                .OfType<Views.SpravciView>().FirstOrDefault()?.Close();
        }
    }
}
