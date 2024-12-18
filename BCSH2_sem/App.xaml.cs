using StromApp.Handlers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BCSH2_sem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Inicializace databáze a vložení počátečních dat
        private SQLiteHandler dbHandler;

        public App()
        {
            dbHandler = new SQLiteHandler();
            dbHandler.InitializeDatabase();
            dbHandler.InsertInitialData();
        }
    }

}
