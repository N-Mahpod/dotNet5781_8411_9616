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
    }
}
