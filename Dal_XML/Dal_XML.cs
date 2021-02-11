using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal_Api;
using Dal_Api.DO;

namespace Dal
{
    sealed class Dal_XML : IDal
    {
        #region singleton
        static readonly Dal_XML instance = new Dal_XML();
        static Dal_XML() { }// static ctor to ensure instance init is done just before first usage
        Dal_XML() { } // default => private
        public static Dal_XML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region User
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int id, Action<User> update)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusesLNs(Func<int, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int id, Action<Bus> update)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Station
        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetStationsKeys(Func<int, object> generate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int key, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus Line
        public BusLine GetBusLine(int key)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetBusLinesKeys(Func<int, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int ln, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusLine(int key)
        {
            throw new NotImplementedException();
        }

        public void ClearBusLines()
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BusLine b)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
