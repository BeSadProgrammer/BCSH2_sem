using System.ComponentModel;

namespace StromApp.Models;

[Serializable]
public class Region : INotifyPropertyChanged
{
    private static int _nextID = 1;

    private int _regionID;
    private string _nazevRegionu;

    public int RegionID
    {
        get => _regionID;
        set
        {
            if (_regionID != value)
            {
                _regionID = value;
                OnPropertyChanged(nameof(RegionID));
            }
        }
    }

    public string NazevRegionu
    {
        get => _nazevRegionu;
        set
        {
            if (_nazevRegionu != value)
            {
                _nazevRegionu = value;
                OnPropertyChanged(nameof(NazevRegionu));
            }
        }
    }

    public Region(string nazevRegionu)
    {
        RegionID = _nextID++;
        NazevRegionu = nazevRegionu;
    }

    public static void SetNextID(int nextID) => _nextID = nextID;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
