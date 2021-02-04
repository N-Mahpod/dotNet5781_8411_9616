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
using System.Collections.ObjectModel;
using BLL.BLL_Api;
using BLL.BLL_Object;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        IBLL bl;
        ObservableCollection<BusLine> ObserListOfLines;
        public LinesWindow(IBLL _bl, ObservableCollection<BusLine> _ObserListOfLines, Line l)
        {
            InitializeComponent();

            bl = _bl;
            ObserListOfLines = _ObserListOfLines;
        }
    }
}
