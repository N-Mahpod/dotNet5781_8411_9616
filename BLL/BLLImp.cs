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

        BLL_Object.Bus IBLL.GetBus(int licenseNum)
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
            string ln = BLL_Object.Bus.MakeLicenseNum(licenseNum,).// Now Im here!!!!!!!!!!!!!!!!!!!!!!!! I didn't finish. good night!
        }

        IEnumerable<BLL_Object.Bus> IBLL.GetAllBuses()
        {
            return from item in dl.GetBusesLNs((ln) => { return GetBus(ln); })
                   let bus = item as BLL_Object.Bus
                   orderby bus.LicenseInt
                   select bus;
        }

        bool IBLL.IsAdmin(string name, string password)
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
