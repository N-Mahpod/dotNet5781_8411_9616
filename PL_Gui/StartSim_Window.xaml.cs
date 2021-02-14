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
    /// Interaction logic for StartSim_Window.xaml
    /// </summary>
    public partial class StartSim_Window : Window
    {
        IBLL bl;
        AdminWindow adw;
        SimulationClock simClk;

        public StartSim_Window(IBLL _bl, AdminWindow _adw)
        {
            InitializeComponent();
            bl = _bl;
            adw = _adw;

            simClk = SimulationClock.Instance;

            sliderSpS.DataContext = simClk;
            tbSpS.DataContext = simClk;
        }
    }
}
