using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal_Api;
using Dal_Api.DO;
using DS;

namespace Dal
{
    sealed class Dal_Object : IDal
    {
        #region singelton
        static readonly Dal_Object instance = new Dal_Object();
        static Dal_Object() { }// static ctor to ensure instance init is done just before first usage
        Dal_Object() { } // default => private
        public static Dal_Object Instance { get => instance; }// The public Instance property to use
        #endregion

        #region User
        void IDal.AddUser(User user)
        {
            throw new NotImplementedException();
        }

        void IDal.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IDal.GetAllUsersBy(Predicate<User> predicate)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IDal.GetAllUsers()
        {
            //return from user in DataSource.ListUsers
            //       select user.Clone();
            
            IEnumerable<User> ul = from u in DataSource.ListUsers
                                   select u.Clone();

            return ul;
        }

        User IDal.GetUser(int id)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateUser(int id, Action<User> update)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        void IDal.AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Bus> IDal.GetAllBuses()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Bus> IDal.GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int ln)
        {
            Dal_Api.DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == ln);

            if (bus != null)
                return bus.Clone();
            else
                throw new Dal_Api.DO.LnNotExistExeption();
        }

        void IDal.UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int ln, Action<Bus> update)
        {
            try
            {
                update(DataSource.ListBuses.Find(b => b.LicenseNum == ln));
            }
            catch
            {
                throw new Dal_Api.DO.LnNotExistExeption();
            }
        }

        void IDal.DeleteBus(int ln)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> IDal.GetBusesLNs(Func<int, object> generate)
        {
            return from bus in DataSource.ListBuses
                   select generate(bus.LicenseNum);
        }
        #endregion

        #region Station
        void IDal.AddStation(Station station)
        {
            DataSource.ListStations.Add(station);
        }

        IEnumerable<Station> IDal.GetAllStations()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Station> IDal.GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> IDal.GetStationsKeys(Func<int, object> generate)
        {
            return from stat in DataSource.ListStations
                   select generate(stat.Key);
        }

        public Station GetStation(int key)
        {
            Dal_Api.DO.Station stat = DataSource.ListStations.Find(s => s.Key == key);

            if (stat != null)
                return stat.Clone();
            else
                throw new Dal_Api.DO.KeyNotExistExeption();
        }

        void IDal.UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateStation(int key, Action<Station> update)
        {
            try
            {
                update(DataSource.ListStations.Find(b => b.Key == key));
            }
            catch
            {
                throw new Dal_Api.DO.KeyNotExistExeption();
            }
        }

        void IDal.DeleteStation(int key)
        {
            int i = DataSource.ListStations.RemoveAll((Station s) => s.Key == key);
            if (i == 0)
                throw new KeyNotExistExeption();
        }
        #endregion

        #region Bus Line
        public BusLine GetBusLine(int key)
        {
            Dal_Api.DO.BusLine bl = DataSource.ListLines.Find(line => line.key == key);

            if (bl != null)
                return bl.Clone();
            else
                throw new Dal_Api.DO.KeyNotExistExeption();
        }
        void IDal.UpdateBusLine(int key, Action<BusLine> update)
        {
            try
            {
                update(DataSource.ListLines.Find(b => b.key == key));
            }
            catch
            {
                throw new Dal_Api.DO.KeyNotExistExeption();
            }
        }
        public IEnumerable<object> GetBusLinesKeys(Func<int, object> generate)
        {
            return from l in DataSource.ListLines
                   select generate(l.key);
        }
        public void DeleteBusLine(int key)
        {
            BusLine l = DataSource.ListLines.Find((BusLine _l) =>
            {
                return _l.key == key;
            });
            DataSource.ListLines.Remove(l);
        }

        public void ClearBusLines()
        {
            DataSource.ListLines.Clear();
        }

        public void AddBusLine(BusLine b)
        {
            DataSource.ListLines.Add(b);
        }
        #endregion
    }
}
