using StromLibrary;
using System;
using System.ComponentModel;

namespace StromApp.Models;

[Serializable]
public class Spravce : INotifyPropertyChanged
{
    private static int _nextID = 1;

    private int _spravceID;
    private string _jmeno;
    private string _prijmeni;
    private string _email;
    private string _telefon;
    private Region _region;

    public int SpravceID
    {
        get => _spravceID;
        set
        {
            if (_spravceID != value)
            {
                _spravceID = value;
                OnPropertyChanged(nameof(SpravceID));
            }
        }
    }

    public string Jmeno
    {
        get => _jmeno;
        set
        {
            if (_jmeno != value)
            {
                _jmeno = value;
                OnPropertyChanged(nameof(Jmeno));
            }
        }
    }

    public string Prijmeni
    {
        get => _prijmeni;
        set
        {
            if (_prijmeni != value)
            {
                _prijmeni = value;
                OnPropertyChanged(nameof(Prijmeni));
            }
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string Telefon
    {
        get => _telefon;
        set
        {
            if (_telefon != value)
            {
                _telefon = value;
                OnPropertyChanged(nameof(Telefon));
            }
        }
    }

    public Region Region
    {
        get => _region;
        set
        {
            if (_region != value)
            {
                _region = value;
                OnPropertyChanged(nameof(Region));
            }
        }
    }

    public Spravce(string jmeno, string prijmeni, string email, string telefon, Region region)
    {
        Jmeno = jmeno;
        Prijmeni = prijmeni;
        Email = email;
        Telefon = telefon;
        Region = region;
        SpravceID = _nextID++;
    }

    public static void SetNextID(int nextID)
    {
        _nextID = nextID;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
