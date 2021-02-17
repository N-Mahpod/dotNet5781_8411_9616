using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    class StationPanel
    {
        #region singelton
        static readonly StationPanel instance = new StationPanel();
        static StationPanel() { }// static ctor to ensure instance init is done just before first usage
        StationPanel() { } // default => private
        public static StationPanel Instance { get => instance; }// The public Instance property to use
        #endregion

        public int StationKey { get; set; }
        public List<LineTiming> LineTimings { get; set; }
        private int timingIdx; //Where in the list are we atm.
        public void remake(int id, List<BusLine> lines, TimeSpan now)//Recreates the panel given the station id.
        {
            timingIdx = 0;
            LineTimings = new List<LineTiming>();
            foreach(BusLine l in lines)
            {
                if (l.IncludeStat(id) == false)
                    continue;
                LineTimings.Add(new LineTiming { LineKey = l.Key, StartAt = l.StartAt, ArriveAt = l.ArriveAt(id), LastStation = l.LastStation });
            }
            Sort();
            updateIdx(now);
        }
        public void Sort()
        {
            LineTimings.Sort();
        }

        public bool updateIdx(TimeSpan now)//Updates current idx, assumes list is sorted. Returns whether idx changed.
        {
            bool changed = false;
            while(timingIdx < LineTimings.Count())
            {
                if (now > LineTimings[timingIdx].ArriveAt)//Passed this.
                {
                    changed = true;
                    timingIdx++;
                }
                else
                    break;
            }
            return changed;
        }

        public List<LineTiming> GetNextLines()
        {
            List<LineTiming> res = new List<LineTiming>();
            for (int i = timingIdx; i < Math.Min(timingIdx + 5, LineTimings.Count()); ++i)
            {
                LineTiming curr = LineTimings[i];
                curr.ArriveAt -= SimulationClock.Instance.NowSimulation;
                res.Add(curr);
            }

            return res;
        }

        public int prevLine()//Returns the id of the line previously here.
        {
            if (timingIdx == 0 || LineTimings.Count() == 0)
                return -1;
            return LineTimings[timingIdx - 1].LineKey;
        }
    }
}
