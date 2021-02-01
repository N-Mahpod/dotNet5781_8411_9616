using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    public class BusLineStation
    {
        public int stationID { get; set; }
        public int prevStationID { get; set; }
        public int NextStationID { get; set; }
    }
}
