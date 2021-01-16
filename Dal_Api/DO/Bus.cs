using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime StartDate { get; set; }
        public Status Status { get; set; }
        public DateTime serviceDate { get; set; }
        public double kmFromService { get; set; }
        public double mileage_km { get; set; }
        public double fuel { get; set; } // A number betwin 0 to 1200 that shows how much km the bus can drive
        public double kmFromRefueling { get; set; }
    }
}
