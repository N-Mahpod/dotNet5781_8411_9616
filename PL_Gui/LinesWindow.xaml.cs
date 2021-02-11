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
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        IBLL bl;
        ObservableCollection<BusLine> ObserListOfLines;
        ObservableCollection<Station> ObserListOfStations;
        BusLine l;
        AdminWindow adwin;

        public LinesWindow(IBLL _bl, ObservableCollection<BusLine> _ObserListOfLines, BusLine _l, AdminWindow _adwin)
        {
            InitializeComponent();

            bl = _bl;
            adwin = _adwin;
            ObserListOfLines = _ObserListOfLines;
            ObserListOfStations = new ObservableCollection<Station>();

            cbLines.DisplayMemberPath = "Key";
            cbLines.DataContext = ObserListOfLines;
            cbLines.SelectedIndex = 0;

            cbArea.ItemsSource = Enum.GetValues(typeof(BLL.BLL_Object.Area));

            lvStations.ItemsSource = ObserListOfStations;
        }

        private void cbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l = cbLines.SelectedItem as BusLine;
            
            if (l == null)
            {
                cbLines.SelectedIndex = 0;
                return;
            }

            gridOneLine.DataContext = l;

            RefreshStatObser();
        }

        private void RefreshStatObser()
        {
            ObserListOfStations.Clear();
            foreach(var s in l.Stations)
            {
                ObserListOfStations.Add(bl.GetStation(s));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            bl.RemoveBusLine(l.Key);
            ObserListOfLines.Remove(l);
        }

        private void AddStatbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveStatButton_Click(object sender, RoutedEventArgs e)
        {
            bl.RemoveStationFromLine(l.Key, ((sender as Button).DataContext as Station).BusStationKey);
            RefreshStatObser();
            adwin.RefreshLineObser();

        }
    }
}
