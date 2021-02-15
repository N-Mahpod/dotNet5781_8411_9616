using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public class BusLine
    {
        private TimeSpan startAt;
        private int key;

        public List<int> Stations { get; set; }//List of station IDs.
        public List<TimeSpan> TimeSpanStations { get; set; }
        public Area Area { get; set; }
        public string FirstStation
        {
            get
            {
                if (Stations.Count == 0)
                    return "NULL";
                return Station.StationKeyFormat(Stations[0]);
            }
        }
        public string LastStation
        {
            get
            {
                if (Stations.Count == 0)
                    return "NULL";
                return Station.StationKeyFormat(Stations[Stations.Count - 1]);
            }
        }
        public int NumStations { get => Stations.Count; }
        public TimeSpan TotalTime
        {
            get
            {
                TimeSpan ts = new TimeSpan();
                for (int i = 0; i < TimeSpanStations.Count - 1; ++i)
                    ts += TimeSpanStations[i];
                return ts;
            }
        }
        public TimeSpan StartAt { get => startAt; }
        public int Key { get => key; }//ID of the line.

        public BusLine(int _key, Area area, TimeSpan _startAt)
        {
            key = _key;
            Area = area;
            startAt = _startAt;
            Stations = new List<int>();
            TimeSpanStations = new List<TimeSpan>();
        }
        public bool IncludeStat(int stationKey)
        {
            return Stations.Contains(stationKey);
        }
        public bool RemoveStat(int stationKey)
        {
            int i = Stations.IndexOf(stationKey);
            if (i < 0)
            {
                return false;
            }
            Stations.RemoveAt(i);
            TimeSpanStations.RemoveAt(i);
            return true;
        }

        public void AddStat(int stationKey, double minutesToNext = 0, int i = -1)
        {
            if (i == -1)
            {
                Stations.Add(stationKey);
                TimeSpanStations.Add(TimeSpan.FromMinutes(minutesToNext));
            }
            else if (i >= 0 && i < NumStations)
            {
                Stations.Insert(i, stationKey);
                TimeSpanStations.Insert(i, TimeSpan.FromMinutes(minutesToNext));
            }
            else
                throw new IndexOutOfRangeException();
        }

        public TimeSpan ArriveAt(int stationKey)
        {
            TimeSpan ts = new TimeSpan(startAt.Hours, startAt.Minutes, startAt.Seconds);
            return ts.Add(TimeTo(stationKey));
        }
        public TimeSpan TimeTo(int stationKey)
        {
            TimeSpan ts = new TimeSpan();
            for (int i = 0; i < NumStations - 1; ++i)
            {
                ts = ts.Add(TimeSpanStations[i]);
                if (Stations[i + 1] == stationKey)
                    return ts;
            }
            throw new KeyNotExistExeption();
        }
    }
}
