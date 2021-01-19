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
        Bus GetBus(int id);
        void UpdateBus(Bus bus);
        void UpdateBus(int id, Action<Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(int id);
        #endregion
    }
}
