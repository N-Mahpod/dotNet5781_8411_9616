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
    }
}
