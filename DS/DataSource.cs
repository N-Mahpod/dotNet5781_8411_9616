using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal_Api.DO;

namespace DS
{
    public static class DataSource
    {
        public static List<User> ListUsers;
        public static List<Bus> ListBuses;
        public static List<Station> ListStations;
        public static List<BusLine> ListLines;

        public static List<List<Tuple<int,double>>> TimeDistanceMatrix;

        public static readonly double FULL_FUEL_TANK = 1200;
        public static readonly double KM_ALLOW_FROM_SERVICE = 20000;
        public static readonly int MINUTES_OF_SERVICE = 24 * 60;
        public static readonly int MINUTES_OF_REFUEL = 2 * 60;
        public static readonly int MAX_KMpH = 50;
        public static readonly int MIN_KMpH = 20;

        static DataSource()
        {
            InitAllLists();
            InitStationMatrix();
        }
        static void InitAllLists()
        {
            ListUsers = new List<User>
            {
                new User
                {
                    UserName = "bob",
                    Password = "123",
                    Admin = true
                }
            };

            ListBuses = new List<Bus>
            {
                new Bus
                {
                    LicenseNum = 1234567,
                    StartDate = new DateTime(2000,1,1),
                    BStatus = Status.Ready,
                    ServiceDate = DateTime.Now,
                    KmFromService = 0,
                    Mileage_km = 0,
                    Fuel = FULL_FUEL_TANK,
                    KmFromRefueling = 0
                },
                new Bus
                {
                    LicenseNum = 12345678,
                    StartDate = new DateTime(2020,1,1),
                    BStatus = Status.Ready,
                    ServiceDate = DateTime.Now,
                    KmFromService = 100,
                    Mileage_km = 4000,
                    Fuel = FULL_FUEL_TANK,
                    KmFromRefueling = 0
                }
            };

            ListStations = new List<Station>
            {
                new Station
                {
                    Key = 0,
                    Adress = "Heifa Mercaz",
                    Longitude = 32.00224,
                    Latitude = 34.7055603
                },
                
                new Station
                {
                    Key = 1,
                    Adress = "Mercazit yerusalaim",
                    Longitude = 33.05324,
                    Latitude = 33.6105603
                },
                
                new Station
                {
                    Key = 2,
                    Adress = "elkana",
                    Longitude = 33.10235,
                    Latitude = 35.8603
                }
            };

            BusLineStation bls_l1_s1 = new BusLineStation { lineID = 1, stationID = 0, prevStationID = -1, NextStationID = 1,  minutesToNext = 6 };
            BusLineStation bls_l1_s2 = new BusLineStation { lineID = 1, stationID = 1, prevStationID = 0,  NextStationID = 2,  minutesToNext = 6.5};
            BusLineStation bls_l1_s3 = new BusLineStation { lineID = 1, stationID = 2, prevStationID = 1,  NextStationID = -1, minutesToNext = 0 };
            
            BusLineStation bls_l2_s1 = new BusLineStation { lineID = 2, stationID = 2, prevStationID = -1, NextStationID = 0, minutesToNext = 7.25};
            BusLineStation bls_l2_s2 = new BusLineStation { lineID = 2, stationID = 0, prevStationID = 2, NextStationID = -1, minutesToNext = 0 };

            ListLines = new List<BusLine>
            {
                new BusLine
                {
                    area = Area.Center,
                    key = 1,
                    stations = new List<BusLineStation>{bls_l1_s1, bls_l1_s2, bls_l1_s3 }
                },

                new BusLine
                {
                    area = Area.Jerusalem,
                    key = 2,
                    stations = new List<BusLineStation>{bls_l2_s1, bls_l2_s2 }
                }
            };

        }

        static void InitStationMatrix()
        {
            Random rand = new Random();
            int size = ListStations.Count();
            TimeDistanceMatrix = new List<List<Tuple<int, double>>>();
            for (int i = 0; i < size; ++i) 
            {
                TimeDistanceMatrix.Add(new List<Tuple<int, double>>());
                for (int j = 0; j < size; ++j) 
                {
                    if(i == j)//Relative to self.
                        TimeDistanceMatrix[i].Add(new Tuple<int, double>(0,0));
                    else if(i > j)//Allready calculated this pair.
                        TimeDistanceMatrix[i].Add(TimeDistanceMatrix[j][i]);
                    else//Need to calculate.
                    {
                        double x1 = ListStations[i].Latitude;
                        double y1 = ListStations[i].Longitude;

                        double x2 = ListStations[j].Latitude;
                        double y2 = ListStations[j].Longitude;

                        double dis = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
                        int time = rand.Next(1, 10);
                        TimeDistanceMatrix[i].Add(new Tuple<int, double>(time, dis));
                    }
                }
            }
        }
    }
}