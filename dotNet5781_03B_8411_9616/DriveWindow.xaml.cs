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
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    public partial class DriveWindow : Window
    {
        MainWindow mw;
        Bus bus;
        public DriveWindow(MainWindow _mw, Bus b)
        {
            InitializeComponent();
            mw = _mw;
            bus = b;

            tbLabel.Text = "Bus number " + b.LicenseNum + " can drive " + b.CanDrive().ToString() + "km.\nHow much km do you want to drive?";
        }

        private void tbKM_KeyUp(object sender, KeyEventArgs e)
        {
            double d = 0;
            bool sucsses = Double.TryParse(tbKM.Text, out d);
            if(d < 0 || !sucsses)
            {
                tbError.Text = "Invalid input. don't do it. arrg.";
                return;
            }
            if (d > bus.CanDrive())
            {
                tbError.Text = "Wow wow wow! Too much!!";
                return;
            }

            tbError.Text = "";
        }

        private void tbKM_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                double d = 0;
                bool sucsses = Double.TryParse(tbKM.Text, out d);
                if (d < 0 || !sucsses)
                {
                    MessageBox.Show("Invalid input. arrg. Try again.");
                    return;
                }
                if (d > bus.CanDrive())
                {
                    MessageBox.Show("Too much! Try again");
                    return;
                }

                mw.buses[MainWindow.SearchBus(mw.buses, bus.LicenseNum)].Drive(d);

                mw.lvBusses.Items.Refresh();

                Close();
            }

        }
    }
}
