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

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for StationPannel_Window.xaml
    /// </summary>
    public partial class StationPannel_Window : Window
    {
        IBLL bl;
        
        
        public StationPannel_Window(IBLL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }
    }
}
