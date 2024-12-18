using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.Views;
using System.Collections.ObjectModel;
using System.Linq;

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
            var dialog = new AddSpravceDialog();
            dialog.ShowDialog();
            Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());
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
        private void EditSpravce()
        {
            if (SelectedSpravce == null) return;

            var dialog = new EditSpravceDialog
            {
                DataContext = new EditSpravceViewModel(
                    SelectedSpravce,
                    new ObservableCollection<Region>(_sqliteHandler.GetAllRegiony()),
                    updatedSpravce =>
                    {
                        _sqliteHandler.UpdateSpravce(updatedSpravce);
                        var index = Spravci.IndexOf(SelectedSpravce);
                        Spravci[index] = updatedSpravce; // Aktualizace v ObservableCollection
                    })
            };
            dialog.ShowDialog();
            Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());
        }


        [RelayCommand]
        private void CloseWindow()
        {
            System.Windows.Application.Current.Windows
                .OfType<Views.SpravciView>().FirstOrDefault()?.Close();
        }
    }
}
