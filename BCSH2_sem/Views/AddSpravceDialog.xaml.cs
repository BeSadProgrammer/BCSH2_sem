using StromApp.ViewModels;
using System.Windows;

namespace StromApp.Views
{
    public partial class AddSpravceDialog : Window
    {
        public AddSpravceDialog()
        {
            InitializeComponent();
            this.DataContext = new AddSpravceViewModel();
        }
    }
}
