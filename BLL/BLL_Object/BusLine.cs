using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public class BusLine
    {
        public int Key { get; set; }//ID of the line.
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
                foreach (TimeSpan t in TimeSpanStations)
                    ts += t;
                return ts;
            }
        }

        public BusLine(int key, Area area)
        {
            Key = key;
            Area = area;
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
    }
}
