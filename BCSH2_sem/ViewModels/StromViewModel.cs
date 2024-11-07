using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BCSH2.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

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

        public ObservableCollection<Strom> Stromy { get; set; }

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

        // ICommands for buttons
        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand DeleteAllCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SpravceCommand { get; }
        public ICommand RegionCommand { get; }

        public StromViewModel()
        {
            Stromy = new ObservableCollection<Strom>();

            NewCommand = new RelayCommand(NewAction);
            EditCommand = new RelayCommand(EditAction);
            DeleteCommand = new RelayCommand(DeleteAction);
            SaveCommand = new RelayCommand(SaveAction);
            LoadCommand = new RelayCommand(LoadAction);
            DeleteAllCommand = new RelayCommand(DeleteAllAction);
            SearchCommand = new RelayCommand(SearchAction);
            SpravceCommand = new RelayCommand(SpravceAction);
            RegionCommand = new RelayCommand(RegionAction);
        }

        private void NewAction()
        {
            // Opravit správce -> public Spravce(string jmeno, string prijmeni, string email, string telefon, Region region)
            // Trees.Add(new Strom(new DruhyStromuTyp(), new Spravce(), DateTime.Now, DateTime.Now, "New Location", 10.5, 0.5, "BarkType"));
            MessageBox.Show("aaaa");
        }

        private void EditAction()
        {
            if (Stromy.Count > 0)
            {
                var StromToEdit = Stromy[0];
                StromToEdit.Lokace = "Updated Location";
            }
        }

        private void DeleteAction()
        {
            if (Stromy.Count > 0)
            {
                Stromy.RemoveAt(0);
            }
        }

        private void SaveAction()
        {
        }

        private void LoadAction()
        {
        }

        private void DeleteAllAction()
        {
            Stromy.Clear();
        }

        private void SearchAction()
        {
        }

        private void SpravceAction()
        {
        }

        private void RegionAction()
        {
        }
    }
}
