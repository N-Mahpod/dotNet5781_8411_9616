using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal_Api.DO;

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

    public static class Enums
    {
        public static Area ToBLArea(this Dal_Api.DO.Area a)
        {
            return (Area)Enum.Parse(typeof(Area), a.ToString());
        }

        public static Dal_Api.DO.Area ToDLArea(this Area a)
        {
            return (Dal_Api.DO.Area)Enum.Parse(typeof(Dal_Api.DO.Area), a.ToString());
        }
    }
}
