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
using System.Collections.ObjectModel;
using BLL.BLL_Api;
using BLL.BLL_Object;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        IBLL bl;
        ObservableCollection<BLL.BLL_Object.Station> ObserListOfStations = new ObservableCollection<BLL.BLL_Object.Station>();
        ObservableCollection<BLL.BLL_Object.BusLine> ObserListOfBusLines = new ObservableCollection<BLL.BLL_Object.BusLine>();

        public StationsWindow(IBLL _bl,ObservableCollection<BLL.BLL_Object.Station> StatList, Station stat = null)
        {
            InitializeComponent();

            bl = _bl;

            ObserListOfStations = StatList;

            cbStations.DisplayMemberPath = "BusStationKeyString";
            cbStations.SelectedIndex = 0;
            cbStations.DataContext = ObserListOfStations;
            
            gridOneStation.DataContext = stat;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BLL.BLL_Object.Station stat = (cbStations.SelectedItem as BLL.BLL_Object.Station);

            gridOneStation.DataContext = stat;
        }
    }
}
