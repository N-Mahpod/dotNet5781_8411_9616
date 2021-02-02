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

        #region Bus
        public BLL_Object.Bus GetBus(int licenseNum)
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
        public IEnumerable<BLL_Object.Bus> GetAllBuses()
        {
            return from item in dl.GetBusesLNs((ln) => { return GetBus(ln); })
                   let bus = item as BLL_Object.Bus
                   orderby bus.LicenseInt
                   select bus;
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
            Dal_Api.DO.BusLine dbl;
            try
            {
                dbl = dl.GetBusLine(key);
            }
            catch(Dal_Api.DO.KeyNotExistExeption)
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
        public IEnumerable<BLL_Object.BusLine> GetAllBusLines()
        {
            return from item in dl.GetBusLinesKeys((key) => { return GetBusLine(key); })
                   let l = item as BLL_Object.BusLine
                   orderby l.Key
                   select l;
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
