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
using BLL.BLL_Api;
using BLL.BLL_Object;
using System.ComponentModel;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for StationPannel_Window.xaml
    /// </summary>
    public partial class StationPannel_Window : Window
    {
        IBLL bl;
        BackgroundWorker bk;
        public StationPannel_Window(IBLL _bl, int id)//Stationd`s id.
        {
            InitializeComponent();
            bl = _bl;

            bl.SetStationPanel(id);
            
        }
    }
}
