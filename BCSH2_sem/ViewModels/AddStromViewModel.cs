using BCSH2_sem;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StromApp.Handlers;
using StromApp.Models;
using StromApp.ViewModels;
using StromApp.Views;
using System.Collections.ObjectModel;
using System.Windows;

public partial class AddStromViewModel : ObservableObject
{
    private SQLiteHandler _sqliteHandler;

    [ObservableProperty]
    private DruhyStromuTyp selectedDruhStromu;

    [ObservableProperty]
    private DateTime datumZasazeni;

    [ObservableProperty]
    private DateTime datumPridaniZaznamu;

    [ObservableProperty]
    private string lokace;

    [ObservableProperty]
    private double vyska;

    [ObservableProperty]
    private double prumerKmeni;

    [ObservableProperty]
    private string typKury;

    [ObservableProperty]
    private ObservableCollection<Spravce> spravci;

    [ObservableProperty]
    private Spravce selectedSpravce;

    // Přidání ObservableCollection pro enum DruhyStromuTyp
    [ObservableProperty]
    private ObservableCollection<DruhyStromuTyp> druhyStromu;

    public AddStromViewModel()
    {
        _sqliteHandler = new SQLiteHandler();
        Spravci = new ObservableCollection<Spravce>(_sqliteHandler.GetAllSpravci());

        // Naplnění kolekce DruhyStromu
        DruhyStromu = new ObservableCollection<DruhyStromuTyp>(Enum.GetValues(typeof(DruhyStromuTyp)).Cast<DruhyStromuTyp>());

        // Nastavení výchozích hodnot pro datum
        DatumZasazeni = DateTime.Now;  // Nastavíme na aktuální datum
        DatumPridaniZaznamu = DateTime.Now;  // Nastavíme na aktuální datum
    }

    [RelayCommand]
    private void AddStrom()
    {
        if (string.IsNullOrWhiteSpace(Lokace))
        {
            MessageBox.Show("Lokace je povinná.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (SelectedSpravce == null)
        {
            MessageBox.Show("Správce je povinný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var newStrom = new Strom
        {
            DruhStromu = SelectedDruhStromu,
            SpravceID = SelectedSpravce.ID,  // Použití vybraného správce
            DatumZasazeni = DatumZasazeni,
            DatumPridaniZaznamu = DatumPridaniZaznamu,
            Lokace = Lokace.Trim(),
            Vyska = Vyska,
            PrumerKmeni = PrumerKmeni,
            TypKury = TypKury?.Trim()
        };

        var addedStrom = _sqliteHandler.AddStrom(newStrom);

        // Přidání stromu do ObservableCollection
        var stromyViewModel = (StromViewModel)App.Current.Windows.OfType<StromView>().FirstOrDefault()?.DataContext;
        stromyViewModel?.Stromy.Add(addedStrom);

        // Zavření dialogu
        CloseDialog();
    }


    [RelayCommand]
    private void Cancel()
    {
        CloseDialog();
    }

    private void CloseDialog()
    {
        var dialog = (AddStromDialog)App.Current.Windows.OfType<AddStromDialog>().FirstOrDefault();
        dialog?.Close();
    }
}
