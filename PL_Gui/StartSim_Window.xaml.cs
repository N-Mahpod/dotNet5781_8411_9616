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
            simClk.Restart();

            sliderSpS.DataContext = simClk;
            tbSpS.DataContext = simClk;
            gClock.DataContext = simClk;

            adw.startStop_button.IsEnabled = true;
            adw.startStop_button.Click += Start_button_Click;
            adw.startSim_button.IsEnabled = false;


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (simClk.Working)
            {
                bool f = MessageBox.Show("Are you sure you want to close the Timer?", "hmm", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
                if (!f)
                {
                    e.Cancel = true;
                    return;
                }
            }
            e.Cancel = false;
            bl.StopSimulator();
            adw.startStop_button.Content = "Start";
            adw.startStop_button.IsEnabled = false;
            adw.tbSimClock.Text = "00:00:00";
            adw.startSim_button.IsEnabled = true;
        }

        public void Start_button_Click(object sender, RoutedEventArgs e)
        {
            if (!simClk.Working)
            {
                Start_button.Content = "Stop";
                adw.startStop_button.Content = "Stop";

                gClock.IsEnabled = false;
                sliderSpS.IsEnabled = false;
                tbSpS.IsEnabled = false;

                bl.StartSimulator(simClk.NowSimulation, simClk.Rate, (ts)=>
                {
                    Dispatcher.Invoke(() =>
                    {
                        tbHours.Text = ts.Hours.ToString();
                        tbMinutes.Text = ts.Minutes.ToString();
                        tbSeconds.Text = ts.Seconds.ToString();

                        adw.tbSimClock.Text = ts.ToString();
                    });
                });
            }
            else
            {
                Start_button.Content = "Start";
                adw.startStop_button.Content = "Start";

                gClock.IsEnabled = true;
                sliderSpS.IsEnabled = true;
                tbSpS.IsEnabled = true;

                bl.StopSimulator();
            }
        }
    }
}
