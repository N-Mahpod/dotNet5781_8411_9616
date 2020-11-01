using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    class BusStation
    {
        private string busStationKey;
        private double latitude;
        private double longitude;
        private string stationAdress;

        private static bool IsProperKey(string k)
        {
            if (k.Length > 6 || !IsNumber(k))
                return false;

            while (k.Length < 6)
            {
                k.Insert(0, "0");
            }

            return true;
        }

        private static bool IsNumber(string n)
        {
            foreach (char ch in n)
            {
                if (ch < '0' || ch > '9')
                    return false;
            }
            return true;
        }
    }
}
