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

            foreach (var item in bl.GetLinesInStation(stat.BusStationKey))
            {
                ObserListOfBusLines.Add(item);
            }
            dgLinesStation.ItemsSource = ObserListOfBusLines;

            gridOneStation.DataContext = stat;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Station ns = bl.AddStation();
            ObserListOfStations.Add(ns);
            cbStations.SelectedItem = ns;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ObserListOfStations.Count <=1)
            {
                MessageBox.Show("You can't remove this poor last station. It will be sad:-/");
                return;
            }    
            bl.RemoveStation((gridOneStation.DataContext as Station).BusStationKey);
            ObserListOfStations.Remove(gridOneStation.DataContext as Station);
            cbStations.SelectedIndex = 0;
        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BLL.BLL_Object.Station stat = (cbStations.SelectedItem as BLL.BLL_Object.Station);

            gridOneStation.DataContext = stat;

            ObserListOfBusLines.Clear();
            foreach (var item in bl.GetLinesInStation(stat.BusStationKey))
            {
                ObserListOfBusLines.Add(item);
            }
        }

        //unnesesery
        //private void UpdateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string newadd = tbAdress.Text;
        //    
        //    double newLat;
        //    bool f = double.TryParse(tbLatitude.Text, out newLat);
        //    if (!f)
        //    {
        //        MessageBox.Show("latitiude isn't correct");
        //        return;
        //    }
        //    
        //    double newLong;
        //    f = double.TryParse(tbLatitude.Text, out newLong);
        //    if (!f)
        //    {
        //        MessageBox.Show("longitiude isn't correct");
        //        return;
        //    }
        //
        //    bl.UpdateStation((cbStations.SelectedItem as BLL.BLL_Object.Station).BusStationKey, newadd, newLong, newLat);
        //}
    }
}
