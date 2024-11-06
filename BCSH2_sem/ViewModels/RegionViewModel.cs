using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCSH2.Models;

namespace BCSH2.ViewModels
{
    public class RegionViewModel : BaseViewModel
    {
        private int _regionID;
        private string _nazevRegionu;

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

        public RegionViewModel(Region region)
        {
            _regionID = region.RegionID;
            _nazevRegionu = region.NazevRegionu;
        }
    }
}
