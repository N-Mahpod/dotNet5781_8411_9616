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
                {
                    if (value > 3600)
                        rate = 3600;
                    else if (value < 1)
                        rate = 1;
                    else
                        rate = value;
                }
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

        public int Hours
        {
            get => nowSim.Hours;
            set
            {
                if(Working)
                    throw new SimulationExeption("you can't change NowSimulation while Working!");

                TimeSpan nts;
                try
                {
                    nts = new TimeSpan(value, nowSim.Minutes, nowSim.Seconds);
                }
                catch(Exception)
                {
                    throw new ArgumentException("nope. try somthing else;");
                }
                nowSim = nts;
            }
        }
        public int Minutes
        {
            get => nowSim.Minutes;
            set
            {
                if (Working)
                    throw new SimulationExeption("you can't change NowSimulation while Working!");


                TimeSpan nts;
                try
                {
                    nts = new TimeSpan(nowSim.Hours, value, nowSim.Seconds);
                }
                catch (Exception)
                {
                    throw new ArgumentException("nope. try somthing else;");
                }

                nowSim = nts;
            }
        }
        public int Seconds
        {
            get => nowSim.Seconds;
            set
            {
                if (Working)
                    throw new SimulationExeption("you can't change NowSimulation while Working!");

                
                TimeSpan nts;
                try
                {
                    nts = new TimeSpan(nowSim.Hours, nowSim.Minutes, value);
                }
                catch (Exception)
                {
                    throw new ArgumentException("nope. try somthing else;");
                }

                nowSim = nts;
            }
        }

        private Action<TimeSpan> UpdateTime;
        private Timer timer;

        public void Restart()
        {
            nowSim = new TimeSpan(0, 0, 0);
            rate = 1;
            Stop();
            timer = new Timer(1000);
        }

        public void Start(TimeSpan startTime, int rate, Action<TimeSpan> updateTime)
        {
            if (working) return;

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
            if (!working) return;
            timer.Stop();
            working = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTime(NowSimulation);
            nowSim = nowSim.Add(TimeSpan.FromSeconds(Rate));
        }
    }
}
