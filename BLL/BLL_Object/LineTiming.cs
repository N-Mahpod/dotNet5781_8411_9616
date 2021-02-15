using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public class LineTiming:IComparable
    {
        public int LineKey { get; set; }
        public TimeSpan StartAt { get; set; }
        public string LastStation { get; set; }
        public TimeSpan ArriveAt { get; set; }

        public int CompareTo(object obj)
        {
            return ArriveAt.CompareTo((obj as LineTiming).ArriveAt);
        }
    }
}
