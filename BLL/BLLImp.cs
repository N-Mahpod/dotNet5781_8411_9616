using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BLL_Api;
using BLL.BLL_Object;
using Dal_Api;
using Dal_Api.DO;


namespace BLL
{
    class BLLImp : IBLL
    {
        IDal dl = Dal_Factory.GetDL();
        List<BLL_Object.Bus> buses;
        bool BusesHasSaved = true;
        List<BLL_Object.BusLine> busLines;
        bool BusLinesHasSaved = true;

        List<int> busesDriving = new List<int>();//List of buses (ID`s) currently driving.

        #region Bus
        public BLL_Object.Bus GetBus(int licenseNum)
        {
            if (buses != null)
            {
                BLL_Object.Bus _b = buses.Find((b) =>
                {
                    return b.LicenseInt == licenseNum;
                });
                if (_b != null)
                    return _b;
                else
                    throw new BLL_Object.LnNotExistExeption("This License Number doesn't exist");
            }
            else
            {
                Dal_Api.DO.Bus db;
                try
                {
                    db = dl.GetBus(licenseNum);
                }
                catch (Dal_Api.DO.LnNotExistExeption)
                {
                    throw new BLL_Object.LnNotExistExeption("This License Number doesn't exist");
                }
                BLL_Object.Bus bb = new BLL_Object.Bus(licenseNum, db.StartDate);
                bb.Restart(licenseNum, db.StartDate, db.ServiceDate, db.KmFromService, db.Mileage_km, db.KmFromRefueling);
                return bb;
            }
        }
        public IEnumerable<BLL_Object.Bus> GetAllBuses()
        {
            if (buses != null)
                return buses;

            IEnumerable<BLL_Object.Bus> lb = from item in dl.GetBusesLNs((ln) => { return GetBus(ln); })
                                             let bus = item as BLL_Object.Bus
                                             orderby bus.LicenseInt
                                             select bus;
            buses = lb.ToList();
            return buses;
        }
        public void DriveBus(int licenseNum, double km)
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~just for testing
            BLL_Object.Bus.MinutesInSecond = 10;
            //~~~~~~~~~~~~~~~~~~~~~~~~~untill here

            BLL_Object.Bus b = GetBus(licenseNum);
            b.Drive(km);
            BusesHasSaved = false;
        }

        public int CountBusesDriving()
        {
            return busesDriving.Count();
        }
        public void updateBusesDriving(int id = -1)
        {
            if (id != -1)
                busesDriving.Add(id);
            busesDriving.RemoveAll(doneDriving);
        }
        //Given a bus`s ID returns if it is done driving or not.
        public bool doneDriving(int id)
        {
            BLL_Object.Bus b = GetBus(id);
            return b.Timer >= b.TimeTarget;
        }

        public void UpdateBus(BLL_Object.Bus b)
        {
            dl.UpdateBus(b.LicenseInt, (Dal_Api.DO.Bus db) =>
             {
                 db.StartDate = b.StartDate;
                 db.ServiceDate = b.GetServiceDate();
                 db.Mileage_km = b.GetMileage_Km();
                 db.KmFromService = b.GetKmFromService();
                 db.KmFromRefueling = b.GetKmFromRefueling();
                 db.Fuel = b.GetFuel();
                 db.BStatus = b.Status.ToDLStatus();
             });
        }
        public void SaveBusesChanges()
        {
            buses.ForEach((b) =>
            {
                UpdateBus(b);
            });
            BusesHasSaved = true;
        }        
        #endregion

        #region Station
        public BLL_Object.Station GetStation(int key)
        {
            Dal_Api.DO.Station ds;
            try
            {
                ds = dl.GetStation(key);
            }
            catch (Dal_Api.DO.KeyNotExistExeption)
            {
                throw new BLL_Object.KeyNotExistExeption("This Bus Station Key doesn't exist");
            }
            BLL_Object.Station bs = new BLL_Object.Station(ds.Longitude, ds.Latitude, ds.Adress, ds.Key);
            return bs;
        }
        public IEnumerable<BLL_Object.Station> GetAllStations()
        {
            return from item in dl.GetStationsKeys((key) => { return GetStation(key); })
                   let stat = item as BLL_Object.Station
                   orderby stat.BusStationKey
                   select stat;
        }
        public void RemoveStation(int key)
        {
            dl.DeleteStation(key);

            foreach (BLL.BLL_Object.BusLine v in GetAllBusLines())
            {
                if(v.RemoveStat(key))
                {
                    dl.UpdateBusLine(v.Key, (Dal_Api.DO.BusLine bl) =>
                     {
                         BusLineStation bs = bl.stations.Find((BusLineStation bls) => bls.stationID == key);
                         int i = bl.stations.IndexOf(bs);
                         if (i > 0)
                         {
                             bl.stations[i - 1].NextStationID = bs.NextStationID;
                             bl.stations[i - 1].minutesToNext += bs.minutesToNext;
                         }
                         if (i < bl.stations.Count - 1)
                             bl.stations[i + 1].prevStationID = bs.prevStationID;
                         if (i == bl.stations.Count - 1 && i > 0)
                         {
                             bl.stations[i - 1].minutesToNext = 0;
                         }
                         bl.stations.Remove(bs);
                     });
                }
            }
        }
        public BLL_Object.Station AddStation()
        {
            BLL_Object.Station ns = new BLL_Object.Station();
            Dal_Api.DO.Station dns = new Dal_Api.DO.Station() { Adress = ns.StationAdress, Key = ns.BusStationKey, Latitude = ns.Latitude, Longitude = ns.Longitude };
            dl.AddStation(dns);
            return ns;
        }
        public void UpdateStation(int key, string newadd, double newlong, double newlat)
        {
            dl.UpdateStation(key, (Dal_Api.DO.Station ds) =>
            {
                ds.Adress = newadd;
                ds.Latitude = newlat;
                ds.Longitude = newlong;
            });
        }
        public IEnumerable<BLL_Object.BusLine> GetLinesInStation(int stationKey)
        {
            return from l in GetAllBusLines()
                   where l.IncludeStat(stationKey)
                   select l;
        }
        #endregion

        #region Bus Line
        public BLL_Object.BusLine GetBusLine(int key)
        {
            if (busLines != null)
            {
                BLL_Object.BusLine l = busLines.Find((_l) =>
                {
                    return _l.Key == key;
                });
                if (l != null)
                    return l;
                else
                    throw new BLL_Object.KeyNotExistExeption("This Bus-Line Key doesn't exist");
            }
            else
            {
                Dal_Api.DO.BusLine dbl;
                try
                {
                    dbl = dl.GetBusLine(key);
                }
                catch (Dal_Api.DO.KeyNotExistExeption)
                {
                    throw new BLL_Object.KeyNotExistExeption("This Bus-Line Key doesn't exist");
                }
                BLL_Object.BusLine bl = new BLL_Object.BusLine(key, dbl.area.ToBLArea());
                foreach (Dal_Api.DO.BusLineStation dbls in dbl.stations)
                {
                    bl.AddStat(dbls.stationID, dbls.minutesToNext);
                }

                return bl;
            }
        }
        public IEnumerable<BLL_Object.BusLine> GetAllBusLines()
        {
            if (busLines != null)
                return busLines;

            IEnumerable<BLL_Object.BusLine> lb = from item in dl.GetBusLinesKeys((key) => { return GetBusLine(key); })
                                                 let l = item as BLL_Object.BusLine
                                                 orderby l.Key
                                                 select l;
            busLines = lb.ToList();
            return busLines;
        }
        public bool RemoveStationFromLine(int lineNum, int stationKey)
        {
            BLL_Object.BusLine l = this.GetBusLine(lineNum);
            bool s = l.RemoveStat(stationKey);

            BusLinesHasSaved = false;
            return s;
        }
        public void UpdateBusLine(BLL_Object.BusLine l)
        {
            dl.UpdateBusLine(l.Key, (Dal_Api.DO.BusLine _l) =>
             {
                 _l.area = l.Area.ToDLArea();
                //this func isn't very יעילה, but it simple.
                _l.stations.Clear();
                 for (int i = 0; i < l.NumStations; ++i)
                 {
                     _l.stations.Add(new BusLineStation()
                     {
                         lineID = l.Key,
                         minutesToNext = l.TimeSpanStations[i].TotalMinutes,
                         stationID = l.Stations[i],
                         prevStationID = (i > 0) ? l.Stations[i - 1] : -1,
                         NextStationID = (i < l.NumStations - 1) ? l.Stations[i + 1] : -1
                     });
                 }
             });
        }
        public void SaveBusLinesChanges()
        {
            if (BusLinesHasSaved)
                return;

            dl.ClearBusLines();
            busLines.ForEach((b) =>
            {
                Dal_Api.DO.BusLine dbl = new Dal_Api.DO.BusLine()
                {
                    key = b.Key,
                    stations = new List<BusLineStation>()
                };
                dl.AddBusLine(dbl);
                UpdateBusLine(b);
            });
            BusLinesHasSaved = true;
        }
        public void AddStationToLine(int lineNum, int stationKey, double minutesToNext)
        {
            BLL_Object.BusLine l = this.GetBusLine(lineNum);
            l.AddStat(stationKey, minutesToNext);

            BusLinesHasSaved = false;
        }
        public void RemoveBusLine(int key)
        {
            busLines.RemoveAll((l) =>
            {
                return l.Key == key;
            });
            BusLinesHasSaved = false;
        }

        public void CreatBusLine(int key, BLL_Object.Area area)
        {
            int i = busLines.FindIndex((l) =>
            {
                return (l.Area == area && l.Key == key);
            });
            if (i>=0)
            {
                throw new AlreadyExistExeption("in this area there is a bus line with this number.");
            }

            BLL_Object.BusLine nl = new BLL_Object.BusLine(key, area);
            busLines.Add(nl);
        }
        #endregion

        public bool IsAdmin(string name, string password)
        {
            IEnumerable<User> usersList = dl.GetAllUsers();
            foreach (User u in usersList)
            {
                if (u.UserName == name && u.Password == password)
                    return u.Admin;
            }
            throw new IncorrectSomethingExeption();
        }
    }
}
