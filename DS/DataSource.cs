using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal_Api.DO;

namespace DS
{
    public static class DataSource
    {
        public static List<User> ListUsers;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            ListUsers = new List<User>
            {
                new User
                {
                    UserName = "bob",
                    Password = "123",
                    Admin = true
                }
            };
        }
    }
}
