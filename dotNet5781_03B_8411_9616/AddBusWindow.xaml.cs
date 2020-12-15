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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        MainWindow mw;
        public Bus bus;

        public AddBusWindow(MainWindow _mw/*, ref Bus _b*/)
        {
            InitializeComponent();
            mw = _mw;
            // b = _b;
            tbDay.Text = mw.nowSimulation.Day.ToString();
            tbMonth.Text = mw.nowSimulation.Month.ToString();
            tbYear.Text = mw.nowSimulation.Year.ToString();

            tbSrvDay.Text = mw.nowSimulation.Day.ToString();
            tbSrvMonth.Text = mw.nowSimulation.Month.ToString();
            tbSrvYear.Text = mw.nowSimulation.Year.ToString();
        }

        private void SubmitBusButton_Click(object sender, RoutedEventArgs e)
        {
            //SubmitBusButton.Content = "Done";

            int d, m, y, sd, sm, sy, ln;
            double milage, kmFrmSrv, fuel;
            DateTime start = new DateTime(), srvDate = new DateTime();
            bool success = true;

            success &= Int32.TryParse(tbDay.Text, out d);
            success &= Int32.TryParse(tbMonth.Text, out m);
            success &= Int32.TryParse(tbYear.Text, out y);
            
            success &= Int32.TryParse(tbSrvDay.Text, out sd);
            success &= Int32.TryParse(tbSrvMonth.Text, out sm);
            success &= Int32.TryParse(tbSrvYear.Text, out sy);

            success &= Int32.TryParse(tbLicenseNumber.Text, out ln);
            success &= Double.TryParse(tbMilage.Text, out milage);
            success &= Double.TryParse(tbKmFrmSrv.Text, out kmFrmSrv);
            success &= Double.TryParse(tbFuel.Text, out fuel);

            if (milage < 0 || kmFrmSrv < 0 || fuel < 0 || fuel > Bus.FULL_FUEL_TANK || ln < 0)
                success = false;

            try
            {
                start = new DateTime(y, m, d);
                srvDate = new DateTime(sy, sm, sd);
            }
            catch (Exception)
            {
                success = false;
            }

            if (start > srvDate || start > mw.nowSimulation || srvDate > mw.nowSimulation)
                success = false;

            if ((start.Year >= 2018) && (tbLicenseNumber.Text.Length != 8) || (start.Year < 2018) && (tbLicenseNumber.Text.Length != 7))
                success = false;

            if(!success)
            {
                tbError.Text = "Error: Invalid input.";
                MessageBox.Show("Error: Invalid input! try again.");
                return;
            }

            if(MainWindow.IsExistLN(mw.buses, tbLicenseNumber.Text))
            {
                tbError.Text = "Error: The license number is already exist.";
                MessageBox.Show("Error: The license number is already exist! try again.");
                return;
            }

            bus = new Bus(tbLicenseNumber.Text, start, true);
            bus.Restart(tbLicenseNumber.Text, start, srvDate, kmFrmSrv, milage, kmFrmSrv);

            //mw.lvBusses.ItemsSource;

            //Dispatcher.Invoke(() =>
            //    {
            //~~mw.buses.Add(b);

            //    });
            //~~mw.lvBusses.Items.Refresh();

            //mw.lvBusses.
            ///
            //Needs test against the simulation clock!
            ///



            //~~~~~~~~> Close <~~~~~~~~~~~~|
            
            Close();
        }

        private void tbYear_KeyUp(object sender, KeyEventArgs e)
        {
            int y = 0;
            bool f = Int32.TryParse(tbYear.Text, out y);

            if (!f)
                return;

            if (y < 2018)
                tbDigits.Text = "(7 digits)";
            else
                tbDigits.Text = "(8 digits)";
        }
    }
}
