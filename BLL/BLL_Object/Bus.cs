﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace BLL.BLL_Object
{
    public class Bus
    {
        #region Properties
        private Status status;
        private int licenseNum;
        private DateTime startDate;
        private DateTime serviceDate;
        private double kmFromService;
        private double mileage_km;
        private double fuel; // A number betwin 0 to 1200 that shows how much km the bus can drive
        private double kmFromRefueling;
        #endregion

        #region Consts
        public const double FULL_FUEL_TANK = 1200;
        public const double KM_ALLOW_FROM_SERVICE = 20000;
        public const int MINUTES_OF_SERVICE = 24 * 60;
        public const int MINUTES_OF_REFUEL = 2 * 60;
        public const int MAX_KMpH = 60;
        public const int MIN_KMpH = 20;
        #endregion

        public static DateTime NowSimulation;
        private static bool first = true;
        private static int minutesInSecond;
        private static bool simulation;

        System.Timers.Timer th;
        Thread hlpT;
        private double timer = 0;
        private double timeTarget = 0;
        
        private Random rand;


        #region Constructors
        public Bus(int _licenseNum, DateTime start, bool _simaulation = false, DateTime _nowSimulation = new DateTime(), int minutes_in_second = 0)
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
        public void Restart(int _licenseNum, DateTime start, DateTime service, double _kmFromService, double _mileage_km, double _kmFromRefueling)
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
        #endregion

        #region Status S/Getters
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
        #endregion
        #region Can Drive
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
        #endregion
        
        #region Getters
        public double Timer { get => timer; set => timer = value; }
        public double TimeTarget { get => timeTarget; set => timeTarget = value; }
        public static int MinutesInSecond
        { 
            get => minutesInSecond;
            set
            {
                minutesInSecond = value;
                simulation = true;
                if (NowSimulation == null)
                    NowSimulation = DateTime.Now;
                first = false;
            }
        }
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
            get => MakeLicenseNum(licenseNum, StartDate);
        }
        public int LicenseInt
        {
            get => licenseNum;
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
        #endregion

        #region Change
        public void ChangeLicenseNum(int ln)
        {
            ChangeLicenseNum(ln, StartDate);
        }
        public void ChangeLicenseNum(int ln, DateTime start)
        {
            if (((start.Year < 2018) && (ln.ToString().Length > 7)) || (ln.ToString().Length > 8))
                throw new TooLongNumExeption();
            licenseNum = ln;
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
        #endregion

        #region Acts
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
        }
        public void Drive(double km)
        {
            if (status != Status.Ready)
                throw new NotReadyException("This bus can't drive now.");
            if (!CanDrive(km))
                return;

            status = Status.Driving;

            int kmph = rand.Next(MIN_KMpH, MAX_KMpH);
            timeTarget = (km / kmph) * 60;
            DriveWithoutChecking(km);

            DoInTime();
            bool h = IsNeedRefuel;
            h = IsInDanger;
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
        #endregion

        #region Static Funcs - LicenseNum
        // The next func takes a string of a number and a date and return a string with those "-" (1234567 -> 12-345-67).
        public static string MakeLicenseNum(int _licenseNum, DateTime start)
        {
            if (start.Year < 2018)
            {
                // if the input was wrong
                return string.Format("{0:00-000-00}", _licenseNum);
            }
            else
            {
                return string.Format("{0:000-00-000}", _licenseNum);
            }
        }
        public static bool IsLicenseNumFormat(string _licenseNum)
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
        #endregion

        public override string ToString()
        {
            string s = "Bus Number: " + licenseNum.ToString()
                     + ".\tStatus: " + status.ToString()
                     + ((status == Status.Ready) ?
                     (".\tCan Drive: " + ((CanDrive().ToString().Length > 7) ? CanDrive().ToString().Remove(7) : CanDrive().ToString())) : "")
                     + "\n";
            return s;
        }
    }
}
