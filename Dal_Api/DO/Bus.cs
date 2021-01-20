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
        public Status BStatus { get; set; }
        public DateTime ServiceDate { get; set; }
        public double KmFromService { get; set; }
        public double Mileage_km { get; set; }
        public double Fuel { get; set; } // A number betwin 0 to 1200 that shows how much km the bus can drive
        public double KmFromRefueling { get; set; }
    }
}
