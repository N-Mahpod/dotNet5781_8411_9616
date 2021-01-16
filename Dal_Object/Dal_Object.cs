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
            return from user in DataSource.ListUsers
                   select user.Clone();
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

        IEnumerable<User> IDal.GetAllBuses()
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IDal.GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        User IDal.GetBus(int id)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        void IDal.UpdateBus(int id, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        void IDal.DeleteBus(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
