using StromApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace StromApp.ViewModels
{
    public partial class RegionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Region> regiony;

        public RegionViewModel()
        {
            // Assuming there is a method to get the list of regions
            Regiony = new ObservableCollection<Region>(GetRegions());
        }

        private IEnumerable<Region> GetRegions()
        {
            // Replace this with the actual logic to retrieve regions
            return new List<Region>();
        }
    }
}
