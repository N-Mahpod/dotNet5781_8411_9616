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
using BLL.BLL_Object;
using BLL.BLL_Api;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    public partial class DriveWindow : Window
    {
        IBLL bl;
        Bus bus;
        public DriveWindow(IBLL _bl, Bus b)
        {
            InitializeComponent();
            bl = _bl;
            bus = b;

            tbLabel.Text = "Bus number " + b.LicenseNum + " can drive " + b.CanDrive().ToString() + "km.\nHow much km do you want to drive?";
        }

        private void tbKM_KeyUp(object sender, KeyEventArgs e)
        {
            double d = 0;
            bool sucsses = Double.TryParse(tbKM.Text, out d);
            if (d < 0 || !sucsses)
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

                bl.DriveBus(bus.LicenseInt, d);

                Close();
            }

        }
    }
}
