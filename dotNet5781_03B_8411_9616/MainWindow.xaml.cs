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
using dotNet5781_01_8411_9616;


namespace dotNet5781_03B_8411_9616
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bus> buses;
        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();

            buses = new List<Bus>();

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
                } while (start > DateTime.Now);

                do {
                    if (start.Year < 2018)
                        license_num = rand.Next(1000000, 9999999);
                    else
                        license_num = rand.Next(10000000, 99999999);
                } while (!IsExistLN(license_num.ToString()));

                bus = new Bus(license_num.ToString(), start);

                bus.Service();
                bus.Refuling();
                buses.Add(bus);
            }

            buses[0].DriveWithoutChecking(buses[0].GetFuel() - 5);
            buses[1].DriveWithoutChecking(Bus.KM_ALLOW_FROM_SERVICE + 50);
            start = DateTime.Now.AddYears(-1);
            start.AddDays(-3);
            buses[1].ChangeService(start);
            buses[1].Refuling();
            buses[2].DriveWithoutChecking(Bus.KM_ALLOW_FROM_SERVICE - 10);


        }

        public bool IsExistLN(string license_num)
        {
            if (SearchBus(license_num) == -1)
                return false;
            return true;
        }

        public int SearchBus(string license_num)
        {
            string ln = license_num.Replace("-", string.Empty);

            for (int i = 0; i < buses.Count; ++i)
            {
                if (ln == buses[i].GetLicenseNum().Replace("-", string.Empty))
                    return i;
            }
            return -1;
        }
    }
}
