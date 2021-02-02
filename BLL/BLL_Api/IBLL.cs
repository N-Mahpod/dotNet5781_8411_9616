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

        #region Station
        BLL_Object.Station GetStation(int key);
        IEnumerable<BLL_Object.Station> GetAllStations();
        void RemoveStation(int key);
        BLL_Object.Station AddStation();
        void UpdateStation(int key, string newadd, double newlong, double newlat);
        IEnumerable<BLL_Object.BusLine> GetLinesInStation(int stationKey);
        #endregion

        BLL_Object.BusLine GetBusLine(int key);
        IEnumerable<BLL_Object.BusLine> GetAllBusLines();
    }
}
