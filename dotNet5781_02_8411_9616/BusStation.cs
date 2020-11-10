using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    class BusStation
    {
        // private variables
        protected static int busStationCounter = 1;
        protected int busStationKey;
        protected string busStationKeyString;
        protected double longitude;
        protected double latitude;
        protected string stationAdress;
        
        // a real rand num.
        private static Random r = new Random();


        // private methods
        // makes a proper key string
        private static bool IsProperKey(ref string k)
        {
            if (k.Length > 6 || !IsNumber(k))
                return false;

            while (k.Length < 6)
            {
                k = k.Insert(0, "0");
            }

            return true;
        }

        // checks if the string is num
        private static bool IsNumber(string n)
        {
            foreach (char ch in n)
            {
                if (ch < '0' || ch > '9')
                    return false;
            }
            return true;
        }

        // sets a rand val for the stat
        private void RandStation()
        {
            busStationKey = busStationCounter;
            busStationKeyString = busStationKey.ToString();
            IsProperKey(ref busStationKeyString);

            busStationCounter++;

            latitude = GetRandomNumber(31, 33.3);
            longitude = GetRandomNumber(34.3, 35.3);

        }
        
        // public methods
        public double Longitude { get => longitude; set => longitude = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public string StationAdress { get => stationAdress; set => stationAdress = value; }
 
        public BusStation(string adress = "")
        {
            RandStation();
            stationAdress = adress;
        }

        public BusStation(double _longitude, double _latitude, string adress = "")
        {
            busStationKey = busStationCounter;
            busStationKeyString = busStationKey.ToString();
            IsProperKey(ref busStationKeyString);

            busStationCounter++;

            longitude = _longitude;
            latitude = _latitude;
            stationAdress = adress;
        }

        public BusStation(BusStation bs)
        {
            busStationKey = bs.busStationKey;
            busStationKeyString = bs.busStationKeyString;
            longitude = bs.longitude;
            latitude = bs.latitude;
            stationAdress = bs.stationAdress;
        }

        public int GetBusStationKey()
        {
            return busStationKey;
        }

        public string GetBusStationKeyString()
        {
            return busStationKeyString;
        }

        public override string ToString()
        {
            string s = "Bus Station Code: " + busStationKeyString + ","
                + "\tLongitude: " + longitude.ToString() + "dE,"
                + "\tLatitude: " + latitude.ToString() + "dN,"
                + "\tAdress: " + ((stationAdress == "") ? "NULL" : stationAdress);
            return s;
        }

        // in order to get a real random number for litudes.
        public static double GetRandomNumber(double minimum, double maximum)
        {
            return r.NextDouble() * (maximum - minimum) + minimum;
        }

        public override bool Equals(object obj)
        {
            return obj is BusStation station &&
                   busStationKey == station.busStationKey;
        }

        //Utility function to calculate distance between this station and another.
        public double getDistance(in BusStation other)
        {
            return Math.Sqrt(Math.Pow(longitude - other.Longitude, 2) + Math.Pow(latitude - other.Latitude, 2));
        }
    }
}
