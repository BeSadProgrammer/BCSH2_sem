using System;
using System.ComponentModel;

namespace StromApp.Models;

[Serializable]
public class Strom : INotifyPropertyChanged
{
    private static int _nextID = 1;

    private DruhyStromuTyp _stromTyp;
    private Spravce _spravce;
    private DateTime _datumZasazeni;
    private DateTime _datumPridaniZaznamu;
    private string _lokace;
    private double _vyska;
    private double _prumerKmene;
    private string _typKury;
    private int _id;

    public DruhyStromuTyp StromTyp
    {
        get => _stromTyp;
        set
        {
            if (_stromTyp != value)
            {
                _stromTyp = value;
                OnPropertyChanged(nameof(StromTyp));
            }
        }
    }

    public Spravce Spravce
    {
        get => _spravce;
        set
        {
            if (_spravce != value)
            {
                _spravce = value;
                OnPropertyChanged(nameof(Spravce));
            }
        }
    }

    public DateTime DatumZasazeni
    {
        get => _datumZasazeni;
        set
        {
            if (_datumZasazeni != value)
            {
                _datumZasazeni = value;
                OnPropertyChanged(nameof(DatumZasazeni));
            }
        }
    }

    public DateTime DatumPridaniZaznamu
    {
        get => _datumPridaniZaznamu;
        set
        {
            if (_datumPridaniZaznamu != value)
            {
                _datumPridaniZaznamu = value;
                OnPropertyChanged(nameof(DatumPridaniZaznamu));
            }
        }
    }

    public string Lokace
    {
        get => _lokace;
        set
        {
            if (_lokace != value)
            {
                _lokace = value;
                OnPropertyChanged(nameof(Lokace));
            }
        }
    }

    public double Vyska
    {
        get => _vyska;
        set
        {
            if (_vyska != value)
            {
                _vyska = value;
                OnPropertyChanged(nameof(Vyska));
            }
        }
    }

    public double PrumerKmene
    {
        get => _prumerKmene;
        set
        {
            if (_prumerKmene != value)
            {
                _prumerKmene = value;
                OnPropertyChanged(nameof(PrumerKmene));
            }
        }
    }

    public string TypKury
    {
        get => _typKury;
        set
        {
            if (_typKury != value)
            {
                _typKury = value;
                OnPropertyChanged(nameof(TypKury));
            }
        }
    }

    public int ID
    {
        get => _id;
        set
        {
            if (_id != value)
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
    }

    public Strom(DruhyStromuTyp stromTyp, Spravce spravce, DateTime datumZasazeni, DateTime datumPridaniZaznamu, string lokace,
        double vyska, double prumerKmene, string typKury)
    {
        StromTyp = stromTyp;
        Spravce = spravce;
        DatumZasazeni = datumZasazeni;
        DatumPridaniZaznamu = datumPridaniZaznamu;
        Lokace = lokace;
        Vyska = vyska;
        PrumerKmene = prumerKmene;
        TypKury = typKury;
        ID = _nextID++;
    }

    public static void SetNextID(int nextID)
    {
        _nextID = nextID;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
