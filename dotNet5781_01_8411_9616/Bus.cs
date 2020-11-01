using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_8411_9616
{
    class Bus
    {
        public const double FULL_FUEL_TANK = 1200;
        public const double KM_ALLOW_FROM_SERVICE = 20000;


        private string licenseNum;
        private DateTime startDate;
        private DateTime serviceDate;
        private double kmFromService;
        private double mileage_km;
        private double fuel; // A number betwin 0 to 1200 that shows how much km the bus can drive
        private double kmFromRefueling;

        public Bus(string _licenseNum, DateTime start)
        {
            Restart(_licenseNum, start, start, 0, 0, 0);
        }

        public DateTime GetStartDate()
        {
            return startDate;
        }

        public string GetLicenseNum()
        {
            return licenseNum;
        }

        public void ChangeLicenseNum(string ln)
        {
            licenseNum = MakeLicenseNum(ln, startDate);
        }

        public DateTime GetServiceDate()
        {
            return serviceDate;
        }

        public void Service(DateTime dateOfService)
        {
            serviceDate = dateOfService;
            kmFromService = 0;
        }

        public double GetKmFromService()
        {
            return kmFromService;
        }

        public double GetMileage_Km()
        {
            return mileage_km;
        }

        public double GetFuel()
        {
            return fuel;
        }

        public void Refuling(double _fuel = FULL_FUEL_TANK)
        {
            if (fuel + _fuel >= FULL_FUEL_TANK)
            {
                fuel = FULL_FUEL_TANK;
            }
            else
                fuel += _fuel;

            kmFromRefueling = FULL_FUEL_TANK - fuel;
        }

        public double GetKmFromRefueling()
        {
            return kmFromRefueling;
        }

        public void Restart(string _licenseNum, DateTime start, DateTime service, double _kmFromService, double _mileage_km, double _kmFromRefueling)
        {
            licenseNum = MakeLicenseNum(_licenseNum, start);
            startDate = start;
            serviceDate = service;
            kmFromService = _kmFromService;
            mileage_km = _mileage_km;
            fuel = FULL_FUEL_TANK - _kmFromRefueling;
            kmFromRefueling = _kmFromRefueling;
        }

        public bool CanDrive(double km)
        {
            DateTime h = serviceDate;
            h.AddYears(5);

            bool a, b;//, c;
            a = (fuel - km) >= 0;
            b = kmFromService + km <= KM_ALLOW_FROM_SERVICE;
            //c = DateTime.Now < h;

            return a && b;// && c;

            //return ((fuel - km) >= 0) && (kmFromService + km <= KM_ALLOW_FROM_SERVICE) && (DateTime.Now < h);
        }

        public void Drive(double km)
        {
            if (!CanDrive(km))
                return;

            kmFromRefueling += km;
            kmFromService += km;
            mileage_km += km;
            fuel -= km;
        }

        // The next func takes a string of a number and a date and return a string with those "-" (1234567 -> 12-345-67).
        public static string MakeLicenseNum(string _licenseNum, DateTime start)
        {
            if (start.Year < 2018)
            {
                // if the input was wrong
                if (_licenseNum.Length == 8)
                    _licenseNum = _licenseNum.Remove(7);

                // s = [0, 1, 2, 3, 4, 5, 6]
                // s = [0, 1, -, 2, 3, 4, 5, 6]
                _licenseNum = _licenseNum.Insert(2, "-");
                // s = [0, 1, -, 2, 3, 4, -, 5, 6]
                _licenseNum = _licenseNum.Insert(6, "-");
                return _licenseNum;
            }
            else
            {
                // if the input was wrong
                if (_licenseNum.Length == 7)
                    _licenseNum = _licenseNum.Insert(0, "0");

                // s = [0, 1, 2, 3, 4, 5, 6, 7]
                // s = [0, 1, 2, -, 3, 4, 5, 6, 7]
                _licenseNum = _licenseNum.Insert(3, "-");
                // s = [0, 1, 2, -, 3, 4, -, 5, 6, 7]
                _licenseNum = _licenseNum.Insert(6, "-");
                return _licenseNum;
            }
        }

        // for some inputs in the Main file
        public static string MakeLicenseNum(string _licenseNum)
        {
            if (_licenseNum.Length == 7)
            { 
                // s = [0, 1, 2, 3, 4, 5, 6]
                // s = [0, 1, -, 2, 3, 4, 5, 6]
                _licenseNum = _licenseNum.Insert(2, "-");
                // s = [0, 1, -, 2, 3, 4, -, 5, 6]
                _licenseNum = _licenseNum.Insert(6, "-");
                return _licenseNum;
            }
            else
            {
                // s = [0, 1, 2, 3, 4, 5, 6, 7]
                // s = [0, 1, 2, -, 3, 4, 5, 6, 7]
                _licenseNum = _licenseNum.Insert(3, "-");
                // s = [0, 1, 2, -, 3, 4, -, 5, 6, 7]
                _licenseNum = _licenseNum.Insert(6, "-");
                return _licenseNum;
            }
        }
    }
}
