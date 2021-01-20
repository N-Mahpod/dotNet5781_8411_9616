using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Api
{
    public interface IBLL
    {
        bool IsAdmin(string name, string password);

        BLL_Object.Bus GetBus(int licenseNum);
        IEnumerable<BLL_Object.Bus> GetAllBuses();

        BLL_Object.Station GetStation(int key);
        IEnumerable<BLL_Object.Station> GetAllStations();
    }
}
