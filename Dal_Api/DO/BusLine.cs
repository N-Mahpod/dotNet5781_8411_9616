using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    public class BusLine
    {
        public int key { get; set; }//ID of the line.
        public List<BusLineStation> stations { get; set; }
    }
}
