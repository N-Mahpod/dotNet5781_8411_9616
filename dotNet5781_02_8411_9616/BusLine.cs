using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    //Position in the line, speacial treatment for last and first.
    enum POSITION { FIRST = -1, MIDDLE = 0, LAST = 1};
    class BusLineStation : BusStation//, IComparable
    {
        //Distance from previous and next station in this line, respectively.
        private POSITION linePos;
        private double distPrev;
        //private double distNext;
        private double minutesPrev;

        //Getters for parameters.
        public double DistPrev { get => distPrev; }
        //public double DistNext { get => distNext; }
        public double MinutesPrev { get => minutesPrev; }



        //public override bool Equals(object obj)
        //{
        //    return obj is BusLineStation station &&
        //           base.Equals(obj) &&
        //           busStationKey == station.busStationKey &&
        //           distPrev == station.distPrev &&
        //           minutesPrev == station.minutesPrev;
        //}


        // public int CompareTo(object obj)
        // {
        //     if (!(obj is BusLineStation))
        //         throw new ArgumentException();
        // 
        //     BusLineStation b = (BusLineStation)obj;
        //     return minutesPrev.CompareTo(b.minutesPrev);
        // }

        /*public BusLineStation(POSITION _linePos, BusLineStation prev, double minutesFromPrev, in BusLineStation next, in string adress = "")
            : base(adress)
        {
            bool ahbal = PrevFlag;
            distPrev = getDistance(prev);
            minutesPrev = minutesFromPrev;
           
            if(next != null)
                distNext = getDistance(next);
        }*/

        public BusLineStation(POSITION _linePos, BusStation bs, double _distPrev = 0, double tFromPrev = 0)//, double _distNext = 0)
            : base(bs)
        {
            linePos = _linePos;
            if (linePos != POSITION.FIRST)
            {
                distPrev = _distPrev;
                minutesPrev = tFromPrev;
            }
            else
            {
                distPrev = minutesPrev = -1;
            }

            /*if (linePos != POSITION.LAST)
                distNext = _distNext;
            else
                distNext = -1;*/

        }
    }

    enum Area
    {
        Error, General, North, South, Center, Jerusalem
    }

    class BusLine: IComparable
    {
        //The line ID.
        private int id;
        //Starting and finishing stations respectively.
        private BusLineStation start;
        private BusLineStation finish;
        private List<BusLineStation> stations;
        Area area;

        public BusLine(int _id = 0, int _area = 1)
        {
            id = _id;
            area = (Area)_area;
            stations = new List<BusLineStation>();
        }

        public int GetSize() { return stations.Count(); }

        public List<BusLineStation> Stations { get => stations; }

        public BusLineStation Start { get => start; }
        
        public BusLineStation Finish { get => finish; }

        public int ID { get => id; }

        public Area Area { get => area; }

        public string StationsString(string ch = "\n")
        {
            string s = "";
            for (int i = 0; i < stations.Count; ++i)
            {
                s = s + (i + 1).ToString() + ": " + stations[i].GetBusStationKeyString() + "." + ch;
            }
            return s;
        }

        public override string ToString()
        {
            string s
                = "Line Number: " + id.ToString() + ".\n"
                + "Area: " + area.ToString() + ".\n"
                + "Stations: " + StationsString("\t") + "\n";
            return s;
        }

        public void Add(BusLineStation lineStation, int i = -1) // -1 for adding to the end of the list.
        {
            if (i == -1)
                i = stations.Count; 
            
            if (i == 0)
                start = lineStation;
            if (i == stations.Count)
                finish = lineStation;

            stations.Insert(i, lineStation);
        }

        public void Remove(BusLineStation lineStation)
        {
            stations.Remove(lineStation);

            if (stations.Count == 0)
                return;

            if (start == lineStation)
                start = stations[0];
            if (finish == lineStation)
                finish = stations[stations.Count - 1];
        }

        public bool IsInclude(BusLineStation station)
        {
            if (FindStation(station) == -1)
                return false;
            return true;
        }
        public bool IsInclude(int num)
        {
            if (FindStation(num) == -1)
                return false;
            return true;
        }

        public int FindStation(BusLineStation station)
        {
            for (int i = 0; i < stations.Count; ++i)
            {
                if (station.Equals(stations[i]))
                    return i;
            }
            return -1;
        }

        public int FindStation(int num)
        {
            for (int i = 0; i < stations.Count; ++i)
            {
                if (stations[i].GetBusStationKey() == num)
                    return i;
            }
            return -1;
        }

        public BusLineStation this[int i]
        {
            get => stations[i];
            set => stations[i] = value;
        }

        public double DistBetween(BusLineStation station0, BusLineStation station1)
        {
            double dist = 0;
            int end = FindStation(station1);

            for (int i = FindStation(station0); i < end; ++i)
            {
                dist += stations[i + 1].DistPrev;
            }

            return dist;
        }

        public double MinutesBetween(BusLineStation station0, BusLineStation station1)
        {
            double time = 0;
            int end = FindStation(station1);

            for (int i = FindStation(station0); i < end; ++i)
            {
                time += stations[i + 1].MinutesPrev;
            }

            return time;
        }

        public BusLine SubRoute(BusLineStation station0, BusLineStation station1)
        {
            BusLine subLine = new BusLine();

            int end = FindStation(station1);

            for (int i = FindStation(station0); i <= end; ++i)
            {
                subLine.Add(stations[i]);
            }

            return subLine;
        }

        public int CompareTo(object obj)
        {
            BusLine b = (BusLine)obj;
            return MinutesBetween(start, finish).CompareTo(MinutesBetween(b.start, b.finish));
        }
    }
}
