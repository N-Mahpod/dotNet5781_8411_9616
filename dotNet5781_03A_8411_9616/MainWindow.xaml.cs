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
using dotNet5781_02_8411_9616;

namespace dotNet5781_03A_8411_9616
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinesCollection lc;
        string[] addresses;
        BusStation[] bsArr;
        private BusLine currentDisplayBusLine;

        public MainWindow()
        {
            InitializeComponent();

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Program.Rand10Lines(ref lc, ref addresses, ref bsArr);

            cbBusLines.ItemsSource = lc;
            cbBusLines.DisplayMemberPath = "ID";
            cbBusLines.SelectedIndex = 0;
            //ShowBusLine(0);
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).ID);
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = lc[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

    }
}
