using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    class StationPanel
    {
        #region singelton
        static readonly StationPanel instance = new StationPanel();
        static StationPanel() { }// static ctor to ensure instance init is done just before first usage
        StationPanel() { } // default => private
        public static StationPanel Instance { get => instance; }// The public Instance property to use
        #endregion

        public int StationKey { get; set; }
        public List<LineTiming> LineTimings { get; set; }

        public void Sort()
        {
            LineTimings.Sort();
        }
    }
}
