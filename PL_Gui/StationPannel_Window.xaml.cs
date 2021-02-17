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
using System.Collections.ObjectModel;
using System.Threading;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for StationPannel_Window.xaml
    /// </summary>
    public partial class StationPannel_Window : Window
    {
        IBLL bl;
        ObservableCollection<BLL.BLL_Object.BusLine> obsPlastic = new ObservableCollection<BLL.BLL_Object.BusLine>();
        ObservableCollection<BLL.BLL_Object.LineTiming> obsElectronic = new ObservableCollection<BLL.BLL_Object.LineTiming>();
        BackgroundWorker bk;
        SimulationClock clk;
        public StationPannel_Window(IBLL _bl, int id)//Stationd`s id.
        {
            InitializeComponent();
            bl = _bl;
            clk = SimulationClock.Instance;

            bl.SetStationPanel(id);

            Slabel.Content = "Station #" + id;

            foreach(BLL.BLL_Object.BusLine l in bl.GetLinesInStation(id))
            {
                obsPlastic.Add(l);
            }
            lvPannelPlastic.SelectedIndex = 0;
            lvPannelPlastic.ItemsSource = obsPlastic;

            foreach(BLL.BLL_Object.LineTiming lt in bl.GetNextLines())
            {
                obsElectronic.Add(lt);
            }
            lvPannelElectronic.SelectedIndex = 0;
            lvPannelElectronic.ItemsSource = obsElectronic;

            bk = new BackgroundWorker();
            bk.WorkerSupportsCancellation = true;
            bk.WorkerReportsProgress = true;

            bk.DoWork += (object s, DoWorkEventArgs ev) =>
            {
                TimeSpan prevT = new TimeSpan();
                while (bk.CancellationPending == false)
                {
                    if (prevT != clk.NowSimulation)
                    {
                        prevT = clk.NowSimulation;
                        bk.ReportProgress(42);
                    }

                    Thread.Sleep(500);
                }
                ev.Cancel = true;
            };

            bk.ProgressChanged+= (object s, ProgressChangedEventArgs ev) =>
            {
                bl.UpdatePanel();
                int prevId = bl.GetPrevLine();
                if (prevId != -1)
                    tbPrevLine.Text = "Previous line here: " + prevId;

                obsElectronic.Clear();
                foreach (BLL.BLL_Object.LineTiming lt in bl.GetNextLines())
                {
                    obsElectronic.Add(lt);
                }
                lvPannelElectronic.SelectedIndex = 0;
                lvPannelElectronic.ItemsSource = obsElectronic;
            };

            bk.RunWorkerAsync();
        }
    }
}
