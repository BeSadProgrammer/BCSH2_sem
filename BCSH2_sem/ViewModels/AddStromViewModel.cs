using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StromApp.ViewModels
{
    public partial class AddStromViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Spravce> spravci;

        [ObservableProperty]
        private ObservableCollection<DruhyStromuTyp> druhyStromu;

        [ObservableProperty]
        private Spravce? selectedSpravce;

        [ObservableProperty]
        private DruhyStromuTyp selectedDruhStromu;

        [ObservableProperty]
        private DateTime? datumZasazeni;

        [ObservableProperty]
        private DateTime? datumPridaniZaznamu;

        [ObservableProperty]
        private string lokace;

        [ObservableProperty]
        private double vyska;

        [ObservableProperty]
        private double prumerKmeni;

        [ObservableProperty]
        private string typKury;

        private SQLiteHandler _sqliteHandler;

        public AddStromViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
            LoadData();
        }

        private void LoadData()
        {
            Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());
            DruhyStromu = new ObservableCollection<DruhyStromuTyp>(Enum.GetValues(typeof(DruhyStromuTyp))
                .Cast<DruhyStromuTyp>()
                .ToList());
        }

        [RelayCommand]
        private void AddStrom()
        {
            if (string.IsNullOrWhiteSpace(Lokace) || Vyska <= 0 || PrumerKmeni <= 0 || TypKury == null)
            {
                // Add validation or message here
                return;
            }

            var newStrom = new Strom
            {
                DruhStromu = SelectedDruhStromu,
                SpravceID = SelectedSpravce?.ID ?? 1,  // Default to 1 if no manager is selected
                DatumZasazeni = DatumZasazeni ?? DateTime.Now,
                DatumPridaniZaznamu = DatumPridaniZaznamu ?? DateTime.Now,
                Lokace = Lokace.Trim(),
                Vyska = Vyska,
                PrumerKmeni = PrumerKmeni,
                TypKury = TypKury.Trim()
            };

            // Přidání stromu do databáze a získání jeho ID
            var addedStrom = _sqliteHandler.AddStrom(newStrom);

            // Přiřazení správného ID z databáze
            newStrom.ID = addedStrom.ID;

            // Zavření okna dialogu
            CloseWindow();
        }

        [RelayCommand]
        private void Cancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            var window = System.Windows.Application.Current.Windows.OfType<Views.AddStromDialog>().FirstOrDefault();
            window?.Close();
        }
    }
}
