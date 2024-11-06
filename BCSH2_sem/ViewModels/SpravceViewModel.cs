using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BCSH2.Models;

namespace BCSH2.ViewModels
{
    public class SpravceViewModel : BaseViewModel
    {
        private int _spravceID;
        private string _jmeno;
        private string _prijmeni;
        private string _email;
        private string _telefon;
        private Region _region;

        public int SpravceID
        {
            get => _spravceID;
            set => SetProperty(ref _spravceID, value);
        }

        public string Jmeno
        {
            get => _jmeno;
            set => SetProperty(ref _jmeno, value);
        }

        public string Prijmeni
        {
            get => _prijmeni;
            set => SetProperty(ref _prijmeni, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Telefon
        {
            get => _telefon;
            set => SetProperty(ref _telefon, value);
        }

        public Region Region
        {
            get => _region;
            set => SetProperty(ref _region, value);
        }

        public SpravceViewModel(Spravce spravce)
        {
            _spravceID = spravce.SpravceID;
            _jmeno = spravce.Jmeno;
            _prijmeni = spravce.Prijmeni;
            _email = spravce.Email;
            _telefon = spravce.Telefon;
            _region = spravce.Region;
        }
    }
}
