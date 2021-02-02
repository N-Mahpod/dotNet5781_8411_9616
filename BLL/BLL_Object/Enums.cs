using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public enum Status
    {
        Ready, Driving, Refueling, Servicing, Danger, NeedRefuel
    }

    public enum POSITION
    { 
        FIRST = -1, MIDDLE = 0, LAST = 1 
    }

    public enum Area
    {
        Error, General, North, South, Center, Jerusalem
    }
}
