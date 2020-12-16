using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;


namespace dotNet5781_01_8411_9616
{
    public enum Status
    {
        Ready, Driving, Refueling, Servicing, Danger, NeedRefuel
    }


    public class Bus
    {
        public static DateTime NowSimulation;
        private static bool first = true;
        private static int minutesInSecond;
        private bool simulation;

        System.Timers.Timer th;
        Thread hlpT;
        private double timer = 0;
        private double timeTarget = 0;


        public const double FULL_FUEL_TANK = 1200;
        public const double KM_ALLOW_FROM_SERVICE = 20000;
        public const int MINUTES_OF_SERVICE = 24 * 60;
        public const int MINUTES_OF_REFUEL = 2 * 60;
        public const int MAX_KMpH = 50;
        public const int MIN_KMpH = 20;

        private Random rand;

        private Status status;
        private string licenseNum;
        private DateTime startDate;
        private DateTime serviceDate;
        private double kmFromService;
        private double mileage_km;
        private double fuel; // A number betwin 0 to 1200 that shows how much km the bus can drive
        private double kmFromRefueling;


        // ~~~~~~~~~~~~~> Constructors
        public Bus(string _licenseNum, DateTime start, bool _simaulation = false, DateTime _nowSimulation = new DateTime(), int minutes_in_second = 0)
        {
            rand = new Random();
            Restart(_licenseNum, start, start, 0, 0, 0);
            simulation = _simaulation;
            if (first && simulation)
            {
                NowSimulation = _nowSimulation;
                minutesInSecond = minutes_in_second;
            }

            if (first && simulation && _nowSimulation == default(DateTime))
            {
                throw new ArgumentNullException("_nowSimulation", "You forgot to enter the simaulation time.");
            }
            if (first && simulation && minutes_in_second == 0)
            {
                throw new ArgumentNullException("minutes_in_second", "You forgot to enter minutes in one second.");
            }

            if (simulation) first = false;
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

            bool h = IsNeedRefuel;
            h = IsInDanger;
        }
        //~Bus()
        //{
        //    try
        //    {
        //        th.Close();
        //        hlpT.Interrupt();
        //        hlpT.Abort();
        //    }
        //    catch(System.NullReferenceException e1)
        //    {
        //        
        //    }
        //    catch (Exception e2)
        //    {
        //
        //    }
        //}


        // ~~~~~~~~~~~~~> Status S/Getters
        public bool IsReady { get => (status == Status.Ready); }
        public bool IsReadyToService { get => (status == Status.Ready || status == Status.NeedRefuel || status == Status.Danger); }
        public bool IsReadyToDrive { get => (status == Status.Ready) && (CanDrive() > 0); }
        public bool IsReadyToRefuel { get => (status == Status.Ready || status == Status.NeedRefuel || status == Status.Danger) && (fuel < FULL_FUEL_TANK); }
        public bool IsInDanger
        {
            get
            {
                if (simulation && (NowSimulation.AddDays(1) >= GetNextServiceDate()))
                    status = Status.Danger;
                else if (!simulation && (DateTime.Now >= GetNextServiceDate()))
                    status = Status.Danger;

                if (kmFromService >= KM_ALLOW_FROM_SERVICE)
                    status = Status.Danger;


                return (status == Status.Danger);
            }
        }
        public bool IsNeedRefuel
        {
            get
            {
                if (fuel == 0)
                    status = Status.NeedRefuel;

                return (status == Status.NeedRefuel);
            }
        }
        public Status Status { get => status; }
        public void MakeReady()
        {
            status = Status.Ready;
        }

        // ~~~~~~~~~~~~> Getters
        public double Timer { get => timer; set => timer = value; }
        public double TimeTarget { get => timeTarget; set => timeTarget = value; }
        public string BackgroundColor
        {
            get
            {
                switch (status)
                {
                    case Status.Ready:
                        return "Green";
                    //break;
                    case Status.Driving:
                        return "Orange";
                    //break;
                    case Status.Refueling:
                        return "Orange";
                    //break;
                    case Status.Servicing:
                        return "Orange";
                    //break;
                    case Status.Danger:
                        return "Red";
                    //break;
                    case Status.NeedRefuel:
                        return "Red";
                    //break;
                    default:
                        break;
                }
                return "Black";
            }
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
        public DateTime GetStartDate()
        {
            return startDate;
        }

        public string LicenseNum
        {
            get => licenseNum;
        }
        public string GetLicenseNum()
        {
            return licenseNum;
        }
        public int GetLicenseInt()
        {
            string h = licenseNum.Replace("-", string.Empty);
            int ln = 0;
            int.TryParse(h, out ln);
            return ln;
        }

        public DateTime GetServiceDate()
        {
            return serviceDate;
        }
        public DateTime GetNextServiceDate()
        {
            return serviceDate.AddYears(1);
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
        public double GetKmFromRefueling()
        {
            return kmFromRefueling;
        }

        private DateTime Now()
        {
            if (simulation)
                return NowSimulation;
            return DateTime.Now;
        }



        // ~~~~~~~~~~~~~~> Change
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

        // ~~~~~~~~~~~~~~> Acts
        public void Service(bool hagdara = false)
        {
            if (status != Status.Ready && status != Status.Danger)
                throw new NotReadyException("This bus can't get service now.");

            serviceDate = Now();
            kmFromService = 0;
            status = Status.Servicing;

            if (hagdara)
                return;

            timeTarget = MINUTES_OF_SERVICE;
            DoInTime();



            //new Thread(() =>
            //{
            //    while (timer < timeTarget)
            //    {
            //        Thread.Sleep(1000);
            //        timer += minutesInSecond;
            //        if (minutesInSecond == 0)
            //            timer += (double)1 / 60;
            //    }
            //    status = Status.Ready;
            //}).Start();
        }
        public void Refuling(bool hagdara = false, double _fuel = FULL_FUEL_TANK)
        {
            if (!hagdara && status != Status.Ready && status != Status.NeedRefuel && status != Status.Danger)
                throw new NotReadyException("This bus can't refuel now.");
            if (fuel + _fuel >= FULL_FUEL_TANK)
            {
                fuel = FULL_FUEL_TANK;
            }
            else
                fuel += _fuel;

            kmFromRefueling = FULL_FUEL_TANK - fuel;
            status = Status.Refueling;

            if (hagdara)
                return;

            timeTarget = MINUTES_OF_REFUEL;

            DoInTime();
            bool h = IsInDanger;



            //timer = 0;
            //
            //new Thread(() =>
            //{
            //    while (timer < timeTarget)
            //    {
            //        Thread.Sleep(1000);
            //        timer += minutesInSecond;
            //        if (minutesInSecond == 0)
            //            timer += (double)1 / 60;
            //    }
            //    status = Status.Ready;
            //    
            //}).Start();
        }
        public void Drive(double km)
        {
            if (status != Status.Ready)
                throw new NotReadyException("This bus can't drive now.");
            if (!CanDrive(km))
                return;

            status = Status.Driving;
            //DriveWithoutChecking(km)


            //timer = 0;
            int kmph = rand.Next(MIN_KMpH, MAX_KMpH);
            timeTarget = (km / kmph) * 60;
            DriveWithoutChecking(km);

            DoInTime();
            bool h = IsNeedRefuel;
            h = IsInDanger;

            //new Thread(() =>
            //{
            //    while (timer < timeTarget)
            //    {
            //        Thread.Sleep(1000);
            //        if (minutesInSecond != 0)
            //        {
            //            timer += minutesInSecond;
            //            //DriveWithoutChecking(((double)1 / 600) * kmph /** minutesInSecond*/);
            //        }
            //        else
            //        {
            //            timer += (double)1 / 60;
            //            //DriveWithoutChecking(((double)1 / 3600) * kmph);
            //        }
            //    }
            //    status = Status.Ready;
            //   
            //}).Start();
        }
        public void DriveWithoutChecking(double km)
        {
            kmFromRefueling += km;
            kmFromService += km;
            mileage_km += km;
            fuel -= km;
            if (fuel < 0)
                fuel = 0;

            bool h = IsNeedRefuel;
            h = IsInDanger;
        }
        private void DoInTime()
        {
            timer = 0;
            th = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            th.Elapsed += ((Object source, ElapsedEventArgs e) =>
            {
                timer += minutesInSecond;
                if (minutesInSecond == 0)
                    timer += (double)1 / 60;
            });
            th.AutoReset = true;
            th.Enabled = true;

            hlpT = new Thread(() =>
            {
                if (minutesInSecond == 0)
                    Thread.Sleep((int)timeTarget * 60 * 1000);
                else
                    Thread.Sleep(1000 * ((int)timeTarget / minutesInSecond));
                th.Close();
                status = Status.Ready;
                timer = 0;
                timeTarget = 0;
            });
            hlpT.Start();
        }


        // ~~~~~~~~~~~~~> Can Drive
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
        public string CanDrive_H
        {
            get
            {
                string s = ((CanDrive()).ToString().Length > 7) ? ((CanDrive()).ToString().Remove(7)) : (CanDrive()).ToString();
                if (s == "0")
                    return "";
                return s;
            }
        }


        // ~~~~~~~~~~~~~~~> Static Funcs
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