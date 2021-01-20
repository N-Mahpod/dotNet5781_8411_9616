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

        public static readonly double FULL_FUEL_TANK = 1200;
        public static readonly double KM_ALLOW_FROM_SERVICE = 20000;
        public static readonly int MINUTES_OF_SERVICE = 24 * 60;
        public static readonly int MINUTES_OF_REFUEL = 2 * 60;
        public static readonly int MAX_KMpH = 50;
        public static readonly int MIN_KMpH = 20;

        static DataSource()
        {
            InitAllLists();
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
        }
    }
}