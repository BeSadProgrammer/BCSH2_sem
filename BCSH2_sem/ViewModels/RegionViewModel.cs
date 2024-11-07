using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using BCSH2.Models;
using System.Windows;

namespace BCSH2.ViewModels
{
    public class RegionViewModel : BaseViewModel
    {
        private int _regionID;
        private string _nazevRegionu;
        private ObservableCollection<Region> _regions;

        public int RegionID
        {
            get => _regionID;
            set => SetProperty(ref _regionID, value);
        }

        public string NazevRegionu
        {
            get => _nazevRegionu;
            set => SetProperty(ref _nazevRegionu, value);
        }

        public ObservableCollection<Region> Regions
        {
            get => _regions;
            private set => SetProperty(ref _regions, value);
        }

        // ICommand properties
        public ICommand NewRegionCommand { get; }
        public ICommand EditRegionCommand { get; }
        public ICommand DeleteRegionCommand { get; }
        public ICommand CloseCommand { get; }

        public RegionViewModel(Region region)
        {
            _regionID = region.RegionID;
            _nazevRegionu = region.NazevRegionu;
            _regions = new ObservableCollection<Region>();

            // Initialize commands
            NewRegionCommand = new RelayCommand(AddNewRegion);
            EditRegionCommand = new RelayCommand(EditSelectedRegion);
            DeleteRegionCommand = new RelayCommand(DeleteSelectedRegion);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void AddNewRegion()
        {
            // Add a new region with a default name and ID
            Regions.Add(new Region("test"));
        }

        private void EditSelectedRegion()
        {
            // This could be replaced with actual logic to select a region
            if (Regions.Any())
            {
                Region regionToEdit = Regions.First();
                regionToEdit.NazevRegionu = "Aktualizovaný region";
            }
        }

        private void DeleteSelectedRegion()
        {
            if (Regions.Any())
            {
                Regions.RemoveAt(0); // Replace with selected region in UI
            }
        }

        private void CloseWindow()
        {
            // Close logic
            foreach (var window in Application.Current.Windows.OfType<Window>())
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
