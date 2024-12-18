using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StromApp.Views
{
    /// <summary>
    /// Interaction logic for RegionView.xaml
    /// </summary>
    public partial class RegionView : Window
    {
        public RegionView()
        {
            InitializeComponent();
            this.DataContext = new RegionViewModel(); // Set the DataContext to the RegionViewModel
        }
    }
}
