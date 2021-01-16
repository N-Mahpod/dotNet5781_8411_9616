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
    sealed class Dal_XML:IDal
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
        void IDal.AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        void IDal.DeleteBus(int id)
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
        #endregion
    }
}
