using BCSH2_sem;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Windows;

namespace StromApp.ViewModels
{
    public partial class AddRegionViewModel : ObservableObject
    {
        private SQLiteHandler _sqliteHandler;
        private string _regionName;

        public AddRegionViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
        }

        public string RegionName
        {
            get => _regionName;
            set => SetProperty(ref _regionName, value);
        }

        [RelayCommand]
        public void AddRegion()
        {
            if (string.IsNullOrWhiteSpace(RegionName))
            {
                MessageBox.Show("Název regionu nesmí být prázdný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Trim a validace názvu
            RegionName = RegionName.Trim();

            var newRegion = new Region
            {
                NazevRegionu = RegionName
            };

            // Přidání regionu do databáze
            _sqliteHandler.AddRegion(newRegion);

            // Uzavření dialogu
            var regionyViewModel = (RegionyViewModel)App.Current.Windows.OfType<RegionyView>().FirstOrDefault().DataContext;
            regionyViewModel.Regiony.Add(newRegion);
            Cancel();
        }

        [RelayCommand]
        public void Cancel()
        {
            // Zavření dialogu
            ((AddRegionDialog)App.Current.Windows.OfType<AddRegionDialog>().FirstOrDefault())?.Close();
        }
    }
}
