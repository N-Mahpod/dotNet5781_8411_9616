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
            lvTimeSpans.ItemsSource = l.TimeSpanStations;
            RefreshStatObser();

            AddStatGrid.Visibility = Visibility.Hidden;
            AddStatbutton.IsEnabled = true;

            tbStartAt.Text = l.StartAt.ToString();
            tbTotalTime.Text = l.TotalTime.ToString();
        }

        private void RefreshStatObser()
        {
            ObserListOfStations.Clear();
            foreach (var s in l.Stations)
            {
                ObserListOfStations.Add(bl.GetStation(s));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddLine_Window alw = new AddLine_Window(bl);
            alw.ShowDialog();
            adwin.RefreshLineObser();
            cbLines.SelectedItem = ObserListOfLines[ObserListOfLines.Count - 1];
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            bl.RemoveBusLine(l.Key);
            ObserListOfLines.Remove(l);
        }

        private void AddStatbutton_Click(object sender, RoutedEventArgs e)
        {
            AddStatGrid.Visibility = Visibility.Visible;
            //lvAddStat.Items.Clear();
            lvAddStat.ItemsSource = bl.GetAllStations();
            AddStatbutton.IsEnabled = false;
        }

        private void RemoveStatButton_Click(object sender, RoutedEventArgs e)
        {
            bl.RemoveStationFromLine(l.Key, ((sender as Button).DataContext as Station).BusStationKey);
            RefreshStatObser();
            adwin.RefreshLineObser();
            lvTimeSpans.Items.Refresh();

        }

        private void doneStatButton_Click(object sender, RoutedEventArgs e)
        {
            AddStatGrid.Visibility = Visibility.Hidden;
            AddStatbutton.IsEnabled = true;
            gridOneLine.DataContext = null;
            gridOneLine.DataContext = l;

            MessageBox.Show("if you added something write the times now!", "hi!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void chbStat_Checked(object sender, RoutedEventArgs e)
        {
            if (l.Stations.Contains(((sender as CheckBox).DataContext as Station).BusStationKey))
                return;

            bl.AddStationToLine(l.Key, ((sender as CheckBox).DataContext as Station).BusStationKey, 0);
            ObserListOfStations.Add((sender as CheckBox).DataContext as Station);
            lvTimeSpans.Items.Refresh();
        }

        private void tbTimeSpan_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter) || Keyboard.IsKeyDown(Key.Tab))
            {
                TimeSpan ts = new TimeSpan();
                bool f = TimeSpan.TryParse((sender as TextBox).Text, out ts);
                if (!f)
                {
                    lvTimeSpans.Items.Refresh();
                    MessageBox.Show("Invalid input:-(");
                }
                else
                {
                    l.TimeSpanStations[lvTimeSpans.Items.IndexOf((sender as TextBox).DataContext)] = ts;
                    lvTimeSpans.Items.Refresh();
                    tbTotalTime.Text = l.TotalTime.ToString();
                }
            }
        }

        private void chbStat_Unchecked(object sender, RoutedEventArgs e)
        {
            (sender as CheckBox).IsChecked = true;
            MessageBox.Show("you can't remove station from here. if you alredy removed press 'done' and try again");
        }
    }
}
