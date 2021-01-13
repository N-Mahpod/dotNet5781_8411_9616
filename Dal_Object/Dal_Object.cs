using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal_Api;
using Dal_Api.DO;
using DS;

namespace Dal_Object
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
            return from person in DataSource.ListUsers
                   select person.Clone();
        }

        User IDal.GetPerson(int id)
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

    }
}
