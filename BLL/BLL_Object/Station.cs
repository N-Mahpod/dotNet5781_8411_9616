using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public class Station
    {
        protected static int busStationCounter = 1;

        protected int busStationKey;
        protected double longitude;
        protected double latitude;
        protected string stationAdress;

        // a real rand num.
        private static Random r = new Random();


        #region Private Methods
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
            int a;
            return int.TryParse(n, out a);
        }

        // sets a rand val for the stat
        private void RandStation()
        {
            busStationKey = busStationCounter;

            busStationCounter++;

            latitude = GetRandomNumber(31, 33.3);
            longitude = GetRandomNumber(34.3, 35.3);

        }
        #endregion

        #region Constructors
        public Station(string adress = "")
        {
            RandStation();
            stationAdress = adress;
        }
        public Station(double _longitude, double _latitude, string adress = "")
        {
            busStationKey = busStationCounter;

            busStationCounter++;

            longitude = _longitude;
            latitude = _latitude;
            stationAdress = adress;
        }
        public Station(double _longitude, double _latitude, string adress, int key)
        {
            busStationKey = key;

            if (key >= busStationCounter)
                busStationCounter = key + 1;

            longitude = _longitude;
            latitude = _latitude;
            stationAdress = adress;
        }
        public Station(Station bs)
        {
            busStationKey = bs.busStationKey;
            longitude = bs.longitude;
            latitude = bs.latitude;
            stationAdress = bs.stationAdress;
        }
        #endregion

        #region S/Getters
        public double Longitude { get => longitude; set => longitude = value; }
        public string LongitudeInFormat { get => string.Format("{0:0.0000000}", longitude); }
        public double Latitude { get => latitude; set => latitude = value; }
        public string LatitudeInFormat { get => string.Format("{0:0.0000000}", latitude); }
        public string StationAdress { get => stationAdress; set => stationAdress = value; }
        public int BusStationKey
        {
            get => busStationKey;
        }
        public string BusStationKeyString
        {
            get => string.Format("{0:000000 }", busStationKey);
        }
        #endregion

        //Utility function to calculate distance between this station and another.
        public double getDistance(in Station other)
        {
            return Math.Sqrt(Math.Pow(longitude - other.Longitude, 2) + Math.Pow(latitude - other.Latitude, 2));
        }

        public override string ToString()
        {
            string s = "Bus Station Code: " + BusStationKeyString + ","
                + "\tLongitude: " + longitude.ToString().Remove(8) + "dE,"
                + "\tLatitude: " + latitude.ToString().Remove(8) + "dN,"
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
            return obj is Station station &&
                   busStationKey == station.busStationKey &&
                   latitude == station.latitude &&
                   longitude == station.longitude;
        }
    }
}
