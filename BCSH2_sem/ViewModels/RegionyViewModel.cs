using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class RegionyViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Region> regiony;

        [ObservableProperty]
        private Region? selectedRegion;

        private SQLiteHandler _sqliteHandler;

        public RegionyViewModel()
        {
            _sqliteHandler = new SQLiteHandler();
            Regiony = new ObservableCollection<Region>(_sqliteHandler.GetAllRegiony());
        }

        [RelayCommand]
        private void AddRegion()
        {
            var newRegion = new Region
            {
                NazevRegionu = "Nový region"
            };

            // Přidání regionu do databáze a získání jeho ID
            var addedRegion = _sqliteHandler.AddRegion(newRegion);

            // Přiřazení správného ID z databáze
            newRegion.ID = addedRegion.ID;

            // Přidání regionu do ObservableCollection
            Regiony.Add(newRegion);
        }

        [RelayCommand]
        private void DeleteRegion()
        {
            if (SelectedRegion == null) return;
            _sqliteHandler.DeleteRegion(SelectedRegion.ID);
            Regiony.Remove(SelectedRegion);
            SelectedRegion = Regiony.FirstOrDefault();
        }

        [RelayCommand]
        private void CloseWindow()
        {
            System.Windows.Application.Current.Windows
                .OfType<Views.RegionyView>().FirstOrDefault()?.Close();
        }

        [RelayCommand]
        private void ShowAddRegionDialog()
        {
            var addRegionDialog = new AddRegionDialog();
            addRegionDialog.ShowDialog();
        }

    }
}
