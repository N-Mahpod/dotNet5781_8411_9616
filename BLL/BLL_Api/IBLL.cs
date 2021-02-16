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
        void SignUp(string name, string password, string SDSP);
        void CreateWorld();

        #region Bus
        BLL_Object.Bus GetBus(int licenseNum);
        IEnumerable<BLL_Object.Bus> GetAllBuses();
        void DriveBus(int licenseNum, double km);
        int CountBusesDriving();//Returns amount of buses driving (i.e. size of list of buses driving).
        void updateBusesDriving(int id = -1);//Updates the list of buses driving. If given an Id adds it.
        void SaveBusesChanges();
        #endregion

        #region Station
        BLL_Object.Station GetStation(int key);
        IEnumerable<BLL_Object.Station> GetAllStations();
        void RemoveStation(int key);
        BLL_Object.Station AddStation();
        void UpdateStation(int key, string newadd, double newlong, double newlat);
        IEnumerable<BLL_Object.BusLine> GetLinesInStation(int stationKey);

        IEnumerable<BLL_Object.LineTiming> GetLineTimings(int stationKey);
        #endregion

        #region Bus Line
        BLL_Object.BusLine GetBusLine(int key);
        IEnumerable<BLL_Object.BusLine> GetAllBusLines();
        bool RemoveStationFromLine(int lineNum, int stationKey);
        void AddStationToLine(int lineNum, int stationKey, double minutesToNext);
        void RemoveBusLine(int key);
        void CreatBusLine(int key, BLL_Object.Area area, TimeSpan startAt);
        void SaveBusLinesChanges();
        #endregion

        #region Simulator
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        void SetStationPanel(int station, Action<BLL_Object.LineTiming> updateBus);
        #endregion
    }
}
