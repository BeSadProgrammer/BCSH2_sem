using StromApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class SpravceViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Spravce> spravci;

        public SpravceViewModel()
        {
            // Assuming there is a method to get the list of Spravce objects
            Spravci = new ObservableCollection<Spravce>(GetSpravci());
        }

        private IEnumerable<Spravce> GetSpravci()
        {
            // Replace this with the actual implementation to retrieve Spravce objects
            return new List<Spravce>();
        }
    }
}
