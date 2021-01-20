using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    public class Station
    {
        public int Key { get; set; }
        public string Adress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
