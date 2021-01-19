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
                    serviceDate = DateTime.Now,
                    kmFromService = 0,
                    mileage_km = 0,
                    fuel = FULL_FUEL_TANK,
                    kmFromRefueling = 0
                },
                new Bus
                {
                    LicenseNum = 12345678,
                    StartDate = new DateTime(2020,1,1),
                    BStatus = Status.Ready,
                    serviceDate = DateTime.Now,
                    kmFromService = 100,
                    mileage_km = 4000,
                    fuel = FULL_FUEL_TANK,
                    kmFromRefueling = 0
                }
            };
        }
    }
}