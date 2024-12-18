using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Models;
using System;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class EditSpravceViewModel : ObservableObject
    {
        private readonly Action<Spravce> _onSave;
        private readonly ObservableCollection<Region> _regiony;

        [ObservableProperty]
        private Spravce selectedSpravce;

        [ObservableProperty]
        private Region? selectedRegion;

        public ObservableCollection<Region> Regiony => _regiony;

        public EditSpravceViewModel(Spravce spravce, ObservableCollection<Region> regiony, Action<Spravce> onSave)
        {
            SelectedSpravce = spravce;
            _regiony = regiony;
            _onSave = onSave;

            // Předvybrání regionu
            SelectedRegion = regiony.FirstOrDefault(r => r.ID == spravce.RegionID);
        }

        [RelayCommand]
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(SelectedSpravce.Jmeno) || string.IsNullOrWhiteSpace(SelectedSpravce.Prijmeni))
            {
                // Validace jména a příjmení
                throw new InvalidOperationException("Jméno a příjmení nesmí být prázdné.");
            }

            // Uložení regionu
            if (SelectedRegion != null)
            {
                SelectedSpravce.RegionID = SelectedRegion.ID;
                SelectedSpravce.NazevRegionu = SelectedRegion.NazevRegionu;
            }

            _onSave(SelectedSpravce);
            Close();
        }

        [RelayCommand]
        private void Cancel() => Close();

        private void Close()
        {
            System.Windows.Application.Current.Windows
                .OfType<Views.EditSpravceDialog>().FirstOrDefault()?.Close();
        }
    }
}
