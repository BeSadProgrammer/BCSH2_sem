using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Models;
using StromApp.Handlers;
using System;
using System.Linq;
using System.Windows;
using BCSH2_sem;
using StromApp.Views;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class EditStromViewModel : ObservableObject
    {
        private SQLiteHandler _sqliteHandler;

        [ObservableProperty]
        private Strom selectedStrom;  // Tento Strom bude obsahovat původní hodnoty

        [ObservableProperty]
        private DruhyStromuTyp selectedDruhStromu;

        [ObservableProperty]
        private DateTime datumZasazeni;

        [ObservableProperty]
        private DateTime datumPridaniZaznamu;

        [ObservableProperty]
        private string lokace;

        [ObservableProperty]
        private double vyska;

        [ObservableProperty]
        private double prumerKmeni;

        [ObservableProperty]
        private string typKury;

        [ObservableProperty]
        private ObservableCollection<Spravce> spravci;

        [ObservableProperty]
        private Spravce selectedSpravce;

        // Přidání ObservableCollection pro enum DruhyStromuTyp
        [ObservableProperty]
        private ObservableCollection<DruhyStromuTyp> druhyStromu;

        public EditStromViewModel(Strom stromToEdit)
        {
            _sqliteHandler = new SQLiteHandler();
            Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());
            SelectedStrom = stromToEdit;

            // Naplnění kolekce DruhyStromu
            DruhyStromu = new ObservableCollection<DruhyStromuTyp>(Enum.GetValues(typeof(DruhyStromuTyp))
    .Cast<DruhyStromuTyp>()
    .Where(d => d != DruhyStromuTyp.None));


            // Načtení hodnot do příslušných vlastností pro úpravu
            SelectedDruhStromu = stromToEdit.DruhStromu;
            DatumZasazeni = stromToEdit.DatumZasazeni;
            DatumPridaniZaznamu = stromToEdit.DatumPridaniZaznamu;
            Lokace = stromToEdit.Lokace;
            Vyska = stromToEdit.Vyska;
            PrumerKmeni = stromToEdit.PrumerKmeni;
            TypKury = stromToEdit.TypKury;

            // Nastavení správce na základě stromu
            SelectedSpravce = Spravci.FirstOrDefault(s => s.ID == stromToEdit.SpravceID);
        }

        [RelayCommand]
        private void SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(Lokace))
            {
                MessageBox.Show("Lokace je povinná.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedSpravce == null)
            {
                MessageBox.Show("Správce je povinný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Aktualizace hodnot v existujícím stromu
            SelectedStrom.DruhStromu = SelectedDruhStromu;
            SelectedStrom.DatumZasazeni = DatumZasazeni;
            SelectedStrom.DatumPridaniZaznamu = DatumPridaniZaznamu;
            SelectedStrom.Lokace = Lokace.Trim();
            SelectedStrom.Vyska = Vyska;
            SelectedStrom.PrumerKmeni = PrumerKmeni;
            SelectedStrom.TypKury = TypKury?.Trim();
            SelectedStrom.SpravceID = SelectedSpravce.ID;

            // Uložení změn do databáze
            _sqliteHandler.UpdateStrom(SelectedStrom);

            // Aktualizace záznamu ve ViewModelu StromView
            var stromyViewModel = (StromViewModel)App.Current.Windows.OfType<StromView>().FirstOrDefault()?.DataContext;
            var stromToUpdate = stromyViewModel?.Stromy.FirstOrDefault(s => s.ID == SelectedStrom.ID);
            if (stromToUpdate != null)
            {
                stromToUpdate.DruhStromu = SelectedStrom.DruhStromu;
                stromToUpdate.Lokace = SelectedStrom.Lokace;
                stromToUpdate.Vyska = SelectedStrom.Vyska;
                stromToUpdate.PrumerKmeni = SelectedStrom.PrumerKmeni;
                stromToUpdate.TypKury = SelectedStrom.TypKury;
            }

            // Zavření dialogu
            CloseDialog();
        }

        [RelayCommand]
        private void Cancel()
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            var dialog = (EditStromDialog)App.Current.Windows.OfType<EditStromDialog>().FirstOrDefault();
            dialog?.Close();
        }
    }
}
