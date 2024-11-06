using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCSH2.Models;

namespace BCSH2.ViewModels
{
    public class StromViewModel : BaseViewModel
    {
        private int _id;
        private DruhyStromuTyp _stromTyp;
        private Spravce _spravce;
        private DateTime _datumZasazeni;
        private DateTime _datumPridaniZaznamu;
        private string _lokace;
        private double _vyska;
        private double _prumerKmene;
        private string _typKury;

        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public DruhyStromuTyp StromTyp
        {
            get => _stromTyp;
            set => SetProperty(ref _stromTyp, value);
        }

        public Spravce Spravce
        {
            get => _spravce;
            set => SetProperty(ref _spravce, value);
        }

        public DateTime DatumZasazeni
        {
            get => _datumZasazeni;
            set => SetProperty(ref _datumZasazeni, value);
        }

        public DateTime DatumPridaniZaznamu
        {
            get => _datumPridaniZaznamu;
            set => SetProperty(ref _datumPridaniZaznamu, value);
        }

        public string Lokace
        {
            get => _lokace;
            set => SetProperty(ref _lokace, value);
        }

        public double Vyska
        {
            get => _vyska;
            set => SetProperty(ref _vyska, value);
        }

        public double PrumerKmene
        {
            get => _prumerKmene;
            set => SetProperty(ref _prumerKmene, value);
        }

        public string TypKury
        {
            get => _typKury;
            set => SetProperty(ref _typKury, value);
        }

        public StromViewModel(Strom strom)
        {
            _id = strom.ID;
            _stromTyp = strom.StromTyp;
            _spravce = strom.Spravce;
            _datumZasazeni = strom.DatumZasazeni;
            _datumPridaniZaznamu = strom.DatumPridaniZaznamu;
            _lokace = strom.Lokace;
            _vyska = strom.Vyska;
            _prumerKmene = strom.PrumerKmene;
            _typKury = strom.TypKury;
        }
    }
}
