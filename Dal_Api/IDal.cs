using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal_Api.DO;

namespace Dal_Api
{
    public interface IDal
    {
        #region User
        void AddUser(User user);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersBy(Predicate<User> predicate);
        User GetUser(int id);
        void UpdateUser(User user);
        void UpdateUser(int id, Action<User> update); //method that knows to updt specific fields in Person
        void DeleteUser(int id);
        #endregion

        #region Bus
        void AddBus(Bus bus);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        IEnumerable<object> GetBusesLNs(Func<int, object> generate);
        Bus GetBus(int ln);
        void UpdateBus(Bus bus);
        void UpdateBus(int ln, Action<Bus> update);
        void DeleteBus(int ln);
        #endregion

        #region Station
        void AddStation(Station station);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        IEnumerable<object> GetStationsKeys(Func<int, object> generate);
        Station GetStation(int key);
        void UpdateStation(Station station);
        void UpdateStation(int key, Action<Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(int key);
        #endregion

        #region Bus Line
        BusLine GetBusLine(int key);
        void UpdateBusLine(int key, Action<BusLine> update);
        IEnumerable<object> GetBusLinesKeys(Func<int, object> generate);
        void DeleteBusLine(int key);
        void ClearBusLines();
        void AddBusLine(BusLine b);
        #endregion
    }
}
