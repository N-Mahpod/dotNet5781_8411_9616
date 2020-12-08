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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using dotNet5781_01_8411_9616;


namespace dotNet5781_03B_8411_9616
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bus> buses;
        Random rand = new Random();
        public DateTime nowSimulation;
        BackgroundWorker clock;
        

        public MainWindow()
        {
            InitializeComponent();

            buses = new List<Bus>();
            nowSimulation = DateTime.Now;
            Bus bus;
            int year, month, day, license_num;
            DateTime start;
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    year = rand.Next(1950, 2020);
                    month = rand.Next(1, 12);
                    day = rand.Next(1, DateTime.DaysInMonth(year, month));
                    start = new DateTime(year, month, day);
                } while (start > nowSimulation);

                do
                {
                    if (start.Year < 2018)
                        license_num = rand.Next(1000000, 9999999);
                    else
                        license_num = rand.Next(10000000, 99999999);
                } while (IsExistLN(buses, license_num.ToString()));

                bus = new Bus(license_num.ToString(), start, true, nowSimulation, 10);

                bus.Service(true);
                bus.MakeReady();
                bus.Refuling(true);
                bus.MakeReady();
                buses.Add(bus);
            }

            buses[0].DriveWithoutChecking(buses[0].GetFuel() - 5);
            buses[1].DriveWithoutChecking(Bus.KM_ALLOW_FROM_SERVICE + 50);
            start = DateTime.Now.AddYears(-1);
            start.AddDays(-3);
            buses[1].ChangeService(start);
            buses[1].Refuling(true);
            buses[1].MakeReady();
            buses[2].DriveWithoutChecking(Bus.KM_ALLOW_FROM_SERVICE - 10);

            bool h = buses[0].IsInDanger;
            h = buses[1].IsInDanger;
            h = buses[2].IsInDanger;

            h = buses[0].IsNeedRefuel;
            h = buses[1].IsNeedRefuel;
            h = buses[2].IsNeedRefuel;


            //~~~~~~~~~~~~> Clock Sim
            clock = new BackgroundWorker();
            clock.DoWork += Clock_DoWork;
            clock.RunWorkerAsync();

            lvBusses.ItemsSource = buses;
        }

        private void Clock_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Dispatcher.Invoke(() =>
                {
                    btSimClok.Content = nowSimulation.ToShortDateString() + "\n" + nowSimulation.ToLongTimeString();
                });
                Dispatcher.Invoke(() =>
                {
                    lvBusses.Items.Refresh();
                });

                Thread.Sleep(1000);
                nowSimulation = nowSimulation.AddMinutes(10);
                Bus.NowSimulation = Bus.NowSimulation.AddMinutes(10);
            }
        }

        public static bool IsExistLN(List<Bus> _buses, string license_num)
        {
            if (SearchBus(_buses, license_num) == -1)
                return false;
            return true;
        }

        public static int SearchBus(List<Bus> _buses, string license_num)
        {
            string ln = license_num.Replace("-", string.Empty);

            for (int i = 0; i < _buses.Count; ++i)
            {
                if (ln == _buses[i].GetLicenseNum().Replace("-", string.Empty))
                    return i;
            }
            return -1;
        }

        private void AddBusButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow abw = new AddBusWindow(this);
            abw.Show();
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            DriveWindow dw = new DriveWindow(this,(sender as Button).DataContext as Bus);
            dw.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clock.Dispose();
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            ((sender as Button).DataContext as Bus).Refuling();
            lvBusses.Items.Refresh();
        }

        private void lvBusses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus b = (Bus)lvBusses.SelectedItem;

            DetailsWindow dtw = new DetailsWindow(this, b);
            dtw.Show();
        }
    }
}
