using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_8411_9616
{
    public enum Status
    {
        Ready, Driving, Refueling, Servicing, Danger
    }


    public class Bus
    {
        public static DateTime NowSimulation;
        private static bool first = true;
        private bool simulation;

        public const double FULL_FUEL_TANK = 1200;
        public const double KM_ALLOW_FROM_SERVICE = 20000;

        private Status status;
        private string licenseNum;
        private DateTime startDate;
        private DateTime serviceDate;
        private double kmFromService;
        private double mileage_km;
        private double fuel; // A number betwin 0 to 1200 that shows how much km the bus can drive
        private double kmFromRefueling;


        public Bus(string _licenseNum, DateTime start, bool _simaulation = false, DateTime _nowSimulation = new DateTime())
        {
            Restart(_licenseNum, start, start, 0, 0, 0);
            simulation = _simaulation;
            NowSimulation = _nowSimulation;
            
            if (first && simulation && NowSimulation == default(DateTime))
            {
                throw new ArgumentNullException("_nowSimulation", "You forgot to enter the simaulation time.");
            }
            
            if (simulation) first = false;
        }

        public DateTime GetStartDate()
        {
            return startDate;
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {

                if (value >= Now())
                {
                    Restart(licenseNum, Now(), Now(), 0, 0, 0);
                    return;
                }
                startDate = value;
                if (startDate > serviceDate)
                    ChangeService(StartDate);
            }
        }

        public Status Status { get => status; }

        public string GetLicenseNum()
        {
            return licenseNum;
        }

        public string LicenseNum
        {
            get => licenseNum;
        }

        public int GetLicenseInt()
        {
            string h = licenseNum.Replace("-", string.Empty);
            int ln = 0;
            int.TryParse(h, out ln);
            return ln;
        }

        public void ChangeLicenseNum(string ln)
        {
            ChangeLicenseNum(ln, StartDate);
        }

        public void ChangeLicenseNum(string ln, DateTime start)
        {
            if (IsLicenseNum(ln))
            {
                if ((ln.Length == 9 && start.Year < 2018) || (ln.Length == 10 && start.Year >= 2018))
                    licenseNum = ln;
            }
            else
                licenseNum = MakeLicenseNum(ln, start);
        }

        public DateTime GetServiceDate()
        {
            return serviceDate;
        }

        public DateTime GetNextServiceDate()
        {
            return serviceDate.AddYears(1);
        }

        public void ChangeService(DateTime dateOfService)
        {
            serviceDate = dateOfService;
            if (startDate > serviceDate)
                serviceDate = startDate;

            if (Now() > GetNextServiceDate())
            {
                status = Status.Danger;
            }
        }

        public void Service()
        {
            if (status != Status.Ready)
                throw new NotReadyException("This bus can't get service now.");

            serviceDate = Now();
            kmFromService = 0;
            status = Status.Servicing;
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
            if (status != Status.Ready)
                throw new NotReadyException("This bus can't refuel now.");
            if (fuel + _fuel >= FULL_FUEL_TANK)
            {
                fuel = FULL_FUEL_TANK;
            }
            else
                fuel += _fuel;

            kmFromRefueling = FULL_FUEL_TANK - fuel;
            status = Status.Refueling;
        }

        public double GetKmFromRefueling()
        {
            return kmFromRefueling;
        }

        public void Restart(string _licenseNum, DateTime start, DateTime service, double _kmFromService, double _mileage_km, double _kmFromRefueling)
        {
            startDate = start;
            ChangeLicenseNum(_licenseNum);
            ChangeService(service);

            if (_mileage_km < 0)
                _mileage_km = 0;
            mileage_km = _mileage_km;

            if (_kmFromService < 0)
                _kmFromService = 0;
            if (_kmFromService > mileage_km)
                _kmFromService = mileage_km;
            kmFromService = _kmFromService;

            if (_kmFromRefueling < 0)
                _kmFromRefueling = 0;
            if (_kmFromRefueling > mileage_km)
                _kmFromRefueling = mileage_km;
            if (_kmFromRefueling > FULL_FUEL_TANK)
                _kmFromRefueling = FULL_FUEL_TANK;
            fuel = FULL_FUEL_TANK - _kmFromRefueling;
            kmFromRefueling = _kmFromRefueling;

            status = Status.Ready;
        }

        public bool CanDrive(double km)
        {
            if (status != Status.Ready)
                return false;
            if (Now() > GetNextServiceDate())
                return false;

            bool a, b;//, c;
            a = (fuel - km) >= 0;
            b = kmFromService + km <= KM_ALLOW_FROM_SERVICE;
            //c = DateTime.Now < h;

            return a && b;// && c;

            //return ((fuel - km) >= 0) && (kmFromService + km <= KM_ALLOW_FROM_SERVICE) && (DateTime.Now < h);
        }

        public double CanDrive()
        {
            if (status != Status.Ready)
                return 0;
            if (Now() > GetNextServiceDate())
                return 0;
            else if (KM_ALLOW_FROM_SERVICE < kmFromService)
                return 0;
            else if (fuel < KM_ALLOW_FROM_SERVICE - kmFromService)
                return fuel;
            else
                return KM_ALLOW_FROM_SERVICE - kmFromService;
        }

        public double CanDrive_H
        {
            get => CanDrive();
        }


        public void Drive(double km)
        {
            if (status != Status.Ready)
                throw new NotReadyException("This bus can't drive now.");
            if (!CanDrive(km))
                return;
            
            status = Status.Driving;
            DriveWithoutChecking(km);
        }

        public void DriveWithoutChecking(double km)
        {
            kmFromRefueling += km;
            kmFromService += km;
            mileage_km += km;
            fuel -= km;
            if (fuel < 0)
                fuel = 0;
        }

        // The next func takes a string of a number and a date and return a string with those "-" (1234567 -> 12-345-67).
        public static string MakeLicenseNum(string _licenseNum, DateTime start)
        {
            if (IsLicenseNum(_licenseNum))
                _licenseNum = _licenseNum.Replace("-", string.Empty);
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

        // for some inputs in the Main file, but we need input checking
        public static string MakeLicenseNum(string _licenseNum)
        {
            if (IsLicenseNum(_licenseNum))
                return _licenseNum;
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

        public static bool IsLicenseNum(string _licenseNum)
        {
            if (_licenseNum.Length != 9 && _licenseNum.Length != 10)
                return false;
            string h = _licenseNum.Replace("-", string.Empty);
            int ln = 0;
            if (!int.TryParse(h, out ln))
                return false;
            if (h.Length != (_licenseNum.Length - 2))
                return false;

            if (_licenseNum.Length == 9)
            {
                if (_licenseNum[2] != '-' || _licenseNum[6] != '-')
                    return false;
            }
            else if (_licenseNum[3] != '-' || _licenseNum[6] != '-')
                return false;

            return true;
        }

        public void MakeReady()
        {
            status = Status.Ready;
        }

        public override string ToString()
        {
            //string s = "Bus Number: " + licenseNum.ToString()
            //         + ".\nStatus: " + status.ToString()
            //         + ".\nStart Date: " + startDate.ToString()
            //         + ".\nMileage (in km): " + mileage_km.ToString()
            //         + ".\nLast Service Date: " + serviceDate.ToString()
            //         + ".\nKm From Service: " + kmFromService.ToString()
            //         + ".\nFuel (how much km the bus can drive): " + fuel.ToString()
            //         + ".\nKm From Refueling: " + kmFromRefueling.ToString()
            //         + ".\nCan Drive: " + ((CanDrive().ToString().Length > 7) ? CanDrive().ToString().Remove(7) : CanDrive().ToString()) + ".\n";
            string s = "Bus Number: " + licenseNum.ToString()
                     + ".\tStatus: " + status.ToString()
                     + ((status == Status.Ready) ?
                     (".\tCan Drive: " + ((CanDrive().ToString().Length > 7) ? CanDrive().ToString().Remove(7) : CanDrive().ToString())) : "")
                     + "\n";
            return s;
        }

        private DateTime Now()
        {
            if (simulation)
                return NowSimulation;
            return DateTime.Now;
        }

        [Serializable]
        public class NotReadyException : Exception
        {
            public NotReadyException() { }
            public NotReadyException(string message) : base(message) { }
            public NotReadyException(string message, Exception inner) : base(message, inner) { }
            protected NotReadyException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
