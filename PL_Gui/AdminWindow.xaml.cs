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
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;
using BLL.BLL_Api;
using BLL.BLL_Object;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        IBLL bl;
        ObservableCollection<BLL.BLL_Object.Bus> ObserListOfBuses = new ObservableCollection<BLL.BLL_Object.Bus>();
        ObservableCollection<BLL.BLL_Object.Station> ObserListOfStations = new ObservableCollection<BLL.BLL_Object.Station>();
        ObservableCollection<BLL.BLL_Object.BusLine> ObserListOfLines = new ObservableCollection<BLL.BLL_Object.BusLine>();
        BackgroundWorker bk;

        public AdminWindow(IBLL _bl)
        {
            InitializeComponent();
            
            bl = _bl;

            foreach (var item in bl.GetAllBuses())
            {
                ObserListOfBuses.Add(item);
            }
            lvBuses.ItemsSource = ObserListOfBuses;
            lvBuses.SelectedIndex = 0;

            foreach (var item in bl.GetAllStations())
            {
                ObserListOfStations.Add(item);
            }
            lvStations.ItemsSource = ObserListOfStations;
            lvStations.SelectedIndex = 0;

            foreach (var item in bl.GetAllBusLines())
            {
                ObserListOfLines.Add(item);
            }
            lvLines.ItemsSource = ObserListOfLines;
            lvLines.SelectedIndex = 0;

            this.Closing += AdminWindow_Closing;

            bk = new BackgroundWorker();
            bk.WorkerReportsProgress = true;
            bk.WorkerSupportsCancellation = true;
        }

        private void AdminWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            DriveWindow dw = new DriveWindow(bl, b);
            dw.ShowDialog();

            lvBuses.Items.Refresh();

            bl.updateBusesDriving(b.LicenseInt);

            //if (bk.IsBusy)
            //bk.CancelAsync();
            //return;

            if (bk.IsBusy)
                return;

            bk.DoWork += (object s, DoWorkEventArgs ev) =>
            {
                do
                {
                    Dispatcher.Invoke(() =>
                    {
                        lvBuses.Items.Refresh();
                        bl.updateBusesDriving();
                    });
                    Thread.Sleep(1000);
                //} while (b.Timer < b.TimeTarget);
                } while (bl.CountBusesDriving() > 0);

                //We refresh another time after the driving to avoid missing the last refresh in between the sleeping period. 
                Dispatcher.Invoke(() =>
                {
                    lvBuses.Items.Refresh();
                });
            };
            bk.RunWorkerAsync();
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The function doesn't exist yet:(");
        }


        private void lvStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StationsWindow sw = new StationsWindow(bl, ObserListOfStations, lvStations.SelectedItem as Station);
            sw.ShowDialog();

            RefreshLineObser();
            lvLines.ItemsSource = ObserListOfLines;
            lvLines.SelectedIndex = 0;

            lvStations.Items.Refresh();
        }

        private void lvLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LinesWindow sw = new LinesWindow(bl, ObserListOfLines, lvLines.SelectedItem as BusLine, this);
            sw.ShowDialog();

            RefreshLineObser();
            lvLines.ItemsSource = ObserListOfLines;
            lvLines.SelectedIndex = 0;

            lvStations.Items.Refresh();
        }

        public void RefreshLineObser()
        {
            ObserListOfLines.Clear();
            foreach (var item in bl.GetAllBusLines())
            {
                ObserListOfLines.Add(item);
            }
        }

        private void saveChanges_button_Click(object sender, RoutedEventArgs e)
        {
            bl.SaveBusesChanges();
        }
    }
}
