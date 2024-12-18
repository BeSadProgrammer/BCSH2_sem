using BCSH2_sem;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace StromApp.ViewModels
{
    public partial class AddSpravceViewModel : ObservableObject
    {
        private SQLiteHandler _sqliteHandler;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string surname;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private Region selectedRegion;

        [ObservableProperty]
        private ObservableCollection<Region> regiony;

        public AddSpravceViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
            Regiony = new ObservableCollection<Region>(_sqliteHandler.GetAllRegiony());
        }

        [RelayCommand]
        private void AddSpravce()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname))
            {
                MessageBox.Show("Jméno a příjmení jsou povinné.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newSpravce = new Spravce
            {
                Jmeno = Name.Trim(),
                Prijmeni = Surname.Trim(),
                Email = Email?.Trim(),
                Telefon = Phone?.Trim(),
                RegionID = SelectedRegion?.ID ?? 1
            };

            // Přidání správce do databáze
            var addedSpravce = _sqliteHandler.AddSpravce(newSpravce);

            // Přiřazení správného ID z databáze
            newSpravce.ID = addedSpravce.ID;

            // Přidání správce do ObservableCollection
            var spravciViewModel = (SpravciViewModel)App.Current.Windows.OfType<SpravciView>().FirstOrDefault()?.DataContext;
            spravciViewModel?.Spravci.Add(newSpravce);

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
            var dialog = (AddSpravceDialog)App.Current.Windows.OfType<AddSpravceDialog>().FirstOrDefault();
            dialog?.Close();
        }
    }
}
