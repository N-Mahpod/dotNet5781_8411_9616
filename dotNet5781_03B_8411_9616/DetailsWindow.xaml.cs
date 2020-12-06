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
using dotNet5781_01_8411_9616;


namespace dotNet5781_03B_8411_9616
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        MainWindow mw;
        Bus b;

        public DetailsWindow(MainWindow _mw, Bus bus)
        {
            InitializeComponent();

            mw = _mw;
            b = bus;
            h_show();
        }

        private void h_show()
        {
            string s
                = "Bus license number:\t\t\t\t" +                       b.LicenseNum + "\n"
                + "Start date:\t\t\t\t\t" +                             b.StartDate.ToShortDateString() + "\n"
                + "Milage:\t\t\t\t\t\t" +                               b.GetMileage_Km().ToString() + " km\n"
                + "Status:\t\t\t\t\t\t" +                               b.Status.ToString() + "\n"
                + "Last service date:\t\t\t\t" +                        b.GetServiceDate().ToShortDateString() + "\n"
                + "Km from the last sevice:\t\t\t\t" +                  b.GetKmFromService() + "\n"
                + "Next service date:\t\t\t\t" +                        b.GetNextServiceDate().ToShortDateString() + "\n"
                + "Fuel (km the bus can drive without refuling):\t" +   b.GetFuel().ToString() + "\n"
                + "Fuel (%):\t\t\t\t\t" +             (((double)((int)((b.GetFuel() / Bus.FULL_FUEL_TANK) * 10000)) / 100)).ToString() + "%\n"
                + "Can drive:\t\t\t\t\t" +                              b.CanDrive().ToString() + "\n"
                + "\n"
                + "Full fuel tank:\t\t\t\t\t" + Bus.FULL_FUEL_TANK.ToString() + "\n"
                + "Km allow from service:\t\t\t\t" + Bus.KM_ALLOW_FROM_SERVICE.ToString();

            tbDetails.Text = s;

            ButtonGrid.DataContext = null;

            ButtonGrid.DataContext = b;

        }

        private void bDrive_Click(object sender, RoutedEventArgs e)
        {
            DriveWindow dw = new DriveWindow(mw, b);
            dw.Show();

            Close();
        }

        private void bService_Click(object sender, RoutedEventArgs e)
        {
            mw.buses[MainWindow.SearchBus(mw.buses, b.LicenseNum)].Service();

            b = mw.buses[MainWindow.SearchBus(mw.buses, b.LicenseNum)];

            mw.lvBusses.Items.Refresh();

            h_show();
        }

        private void bRefuel_Click(object sender, RoutedEventArgs e)
        {
            mw.buses[MainWindow.SearchBus(mw.buses, b.LicenseNum)].Refuling();

            b = mw.buses[MainWindow.SearchBus(mw.buses, b.LicenseNum)];

            mw.lvBusses.Items.Refresh();

            h_show();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            mw.buses.Remove(b);

            mw.lvBusses.Items.Refresh();

            Close();
        }
    }
}
