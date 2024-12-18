using StromApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using StromApp.Handlers;

namespace StromApp.ViewModels
{
    public partial class SpravceViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Spravce> spravci;

        public SpravceViewModel()
        {
            // Load spravci (managers) from SQLite
            Spravci = new ObservableCollection<Spravce>(SQLiteHandler.GetSpravce());
        }
    }
}
