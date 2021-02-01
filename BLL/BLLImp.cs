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

        public BLL_Object.Bus GetBus(int licenseNum)
        {
            Dal_Api.DO.Bus db;
            try
            {
                db = dl.GetBus(licenseNum);
            }
            catch(Dal_Api.DO.LnNotExistExeption ex)
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

        public BLL_Object.Station GetStation(int key)
        {
            Dal_Api.DO.Station ds;
            try
            {
                ds = dl.GetStation(key);
            }
            catch (Dal_Api.DO.KeyNotExistExeption ex)
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

        public BLL_Object.BusLine GetBusLine(int key)
        {
            Dal_Api.DO.BusLine dbl;
            try
            {
                dbl = dl.GetBusLine(key);
            }
            catch(Dal_Api.DO.KeyNotExistExeption ex)
            {
                throw new BLL_Object.KeyNotExistExeption("This Bus-Line Key doesn't exist");
            }
            BLL_Object.BusLine bl = new BLL_Object.BusLine { key = key, stations = new List<int>() };
            foreach (Dal_Api.DO.BusLineStation dbls in dbl.stations)
            {
                bl.stations.Add(dbls.stationID);
            }

            return bl;
        }

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
