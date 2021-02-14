using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading;
using System.Timers;

namespace BLL.BLL_Object
{
    public class SimulationClock
    {
        #region singelton
        static readonly SimulationClock instance = new SimulationClock();
        static SimulationClock() { }// static ctor to ensure instance init is done just before first usage
        SimulationClock() { } // default => private
        public static SimulationClock Instance { get => instance; }// The public Instance property to use
        #endregion

        private bool working = false;
        public bool Working { get => working; }

        private int rate = 1;
        public int Rate
        {
            get => rate;
            set
            {
                if (!Working)
                    rate = value;
                else
                    throw new SimulationExeption("you can't change Rate while Working!");
            }
        }

        private TimeSpan nowSim = new TimeSpan(0, 0, 0);
        public TimeSpan NowSimulation 
        {
            get => nowSim;
            set
            {
                if (!Working)
                    nowSim = value;
                else
                    throw new SimulationExeption("you can't change NowSimulation while Working!");
            }
        }
        
        private Action<TimeSpan> UpdateTime;
        private Timer timer;

        public void Start(TimeSpan startTime, int rate, Action<TimeSpan> updateTime)
        {
            NowSimulation = startTime;
            Rate = rate;
            UpdateTime = updateTime;

            working = true;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            working = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTime(NowSimulation);
            NowSimulation.Add(TimeSpan.FromSeconds(Rate));
        }
    }
}
