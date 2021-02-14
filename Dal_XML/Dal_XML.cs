using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal_Api;
using Dal_Api.DO;
using DS;

namespace Dal
{
    sealed class Dal_XML : IDal
    {
        #region singleton
        static readonly Dal_XML instance = new Dal_XML();
        static Dal_XML() { }// static ctor to ensure instance init is done just before first usage
        Dal_XML() { } // default => private
        public static Dal_XML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string usersPath = @"UsersXml.xml"; //XElement
        string busLinesPath = @"BusLinesXml.xml"; //XElement

        string busesPath = @"BusesXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string lineTripsPath = @"LineTripsXml.xml"; //XMLSerializer
        #endregion

        // XElement
        #region User
        public void AddUser(User user)
        {
            XElement usersRootElem;

            try
            {
                usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            }
            catch(XMLFileLoadCreateException)
            {
                usersRootElem = new XElement("UsersList");
            }
            XElement per1 = (from p in usersRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == user.ID
                             select p).FirstOrDefault();

            if (per1 != null)
                throw new BadUserIdException(user.ID, "Duplicate user ID");

            XElement personElem = new XElement("User",
                                      new XElement("ID", user.ID),
                                      new XElement("UserName", user.UserName),
                                      new XElement("Admin", user.Admin),
                                      new XElement("Password", user.Password));

            usersRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(usersRootElem, usersPath);
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            return (from u in usersRootElem.Elements()
                    select new User()
                    {
                        ID = Int32.Parse(u.Element("ID").Value),
                        UserName = u.Element("UserName").Value,
                        Admin = bool.Parse(u.Element("Admin").Value),
                        Password = u.Element("Password").Value
                    }
                   );
        }

        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            return (from u in usersRootElem.Elements()
                    let u1 = new User()
                    {
                        ID = Int32.Parse(u.Element("ID").Value),
                        UserName = u.Element("UserName").Value,
                        Admin = bool.Parse(u.Element("Admin").Value),
                        Password = u.Element("Password").Value
                    }
                    where predicate(u1)
                    select u1
                   );
        }

        public User GetUser(int id)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            User p = (from per in usersRootElem.Elements()
                      where int.Parse(per.Element("ID").Value) == id
                      select new User()
                      {
                          ID = Int32.Parse(per.Element("ID").Value),
                          UserName = per.Element("UseName").Value,
                          Admin = bool.Parse(per.Element("Admin").Value),
                          Password = per.Element("Password").Value
                      }
                        ).FirstOrDefault();

            if (p == null)
                throw new BadUserIdException(id, $"bad user id: {id}");

            return p;
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

        //XMLSerializer
        #region Bus
        public void AddBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            if (ListBuses.FirstOrDefault(s => s.LicenseNum == bus.LicenseNum) != null)
                throw new BadBusLicenseNumException("Duplicate Bus ln");

            ListBuses.Add(bus); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }

        public void DeleteBus(int id)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus b = ListBuses.Find(p => p.LicenseNum == id);

            if (b != null)
            {
                ListBuses.Remove(b);
            }
            else
                throw new LnNotExistExeption($"bad Bus License Num: {id}");

            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            return from b in ListBuses
                   select b;
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int id)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus b = ListBuses.Find(p => p.LicenseNum == id);
            if (b != null)
                return b; //no need to Clone()
            else
                throw new LnNotExistExeption($"bad bus license num: {id}");
        }

        public IEnumerable<object> GetBusesLNs(Func<int, object> generate)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            return from b in ListBuses
                   select generate(b.LicenseNum);
        }

        public void UpdateBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus b = ListBuses.Find(p => p.LicenseNum == bus.LicenseNum);
            if (b != null)
            {
                ListBuses.Remove(b);
                ListBuses.Add(bus); //no nee to Clone()
            }
            else
                throw new LnNotExistExeption($"bad License Num: {bus.LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBuses,busesPath);
        }

        public void UpdateBus(int id, Action<Bus> update)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus b = ListBuses.Find(p => p.LicenseNum == id);
            if (b != null)
            {
                ListBuses.Remove(b);
                update(b);
                ListBuses.Add(b); //no nee to Clone()
            }
            else
                throw new LnNotExistExeption($"bad License Num: {id}");

            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        #endregion

        //XMLSerializer
        #region Station
        public void AddStation(Station station)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            if (ListStats.FirstOrDefault(s => s.Key == station.Key) != null)
                throw new BadBusLicenseNumException("Duplicate Station Key");

            ListStats.Add(station); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStats, stationsPath);
        }

        public IEnumerable<Station> GetAllStations()
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            return from s in ListStats
                   select s;
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetStationsKeys(Func<int, object> generate)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            return from s in ListStats
                   select generate(s.Key);
        }

        public Station GetStation(int key)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station s = ListStats.Find(p => p.Key == key);
            if (s != null)
                return s; //no need to Clone()
            else
                throw new KeyNotExistExeption($"bad Station key: {key}");
        }

        public void UpdateStation(Station station)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station s = ListStats.Find(p => p.Key == station.Key);
            if (s != null)
            {
                ListStats.Remove(s);
                ListStats.Add(station); //no nee to Clone()
            }
            else
                throw new KeyNotExistExeption($"bad stationKey: {station.Key}");

            XMLTools.SaveListToXMLSerializer(ListStats, stationsPath);
        }

        public void UpdateStation(int key, Action<Station> update)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station s = ListStats.Find(p => p.Key == key);
            if (s != null)
            {
                ListStats.Remove(s);
                update(s);
                ListStats.Add(s); //no nee to Clone()
            }
            else
                throw new KeyNotExistExeption($"bad stationKey: {key}");

            XMLTools.SaveListToXMLSerializer(ListStats, stationsPath);
        }

        public void DeleteStation(int key)
        {
            List<Station> ListStats = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station s = ListStats.Find(p => p.Key == key);

            if (s != null)
            {
                ListStats.Remove(s);
            }
            else
                throw new KeyNotExistExeption($"bad Station key: {key}");

            XMLTools.SaveListToXMLSerializer(ListStats, stationsPath);
        }
        #endregion

        //XElement
        #region Bus Line
        private List<BusLineStation> XElementToList_BusLineStation(XElement xe)
        {
            List<BusLineStation> lbs = new List<BusLineStation>();
            foreach(XElement x in xe.Elements("BusLineStation"))
            {
                lbs.Add(new BusLineStation
                {
                    lineID = int.Parse(x.Element("lineID").Value),
                    minutesToNext = double.Parse(x.Element("minutesToNext").Value),
                    NextStationID = int.Parse(x.Element("NextStationID").Value),
                    prevStationID = int.Parse(x.Element("prevStationID").Value),
                    stationID = int.Parse(x.Element("stationID").Value)
                });
            }
            return lbs;
        }
        private XElement ListToXElement_BusLineStation(List<BusLineStation> lbs)
        {
            XElement xe = new XElement("stations");
            foreach (BusLineStation s in lbs)
            {
                xe.Add(new XElement("BusLineStation",
                    new XElement("lineID", s.lineID),
                    new XElement("minutesToNext", s.minutesToNext),
                    new XElement("NextStationID", s.NextStationID),
                    new XElement("prevStationID", s.prevStationID),
                    new XElement("stationID", s.stationID)));
            }
            return xe;
        }
        
        public BusLine GetBusLine(int key)
        {
            XElement busLinesRootElem = XMLTools.LoadListFromXMLElement(busLinesPath);

            BusLine _b = (from b in busLinesRootElem.Elements()
                          where int.Parse(b.Element("key").Value) == key
                          select new BusLine()
                          {
                              key = Int32.Parse(b.Element("key").Value),
                              area = (Area)Enum.Parse(typeof(Area), b.Element("area").Value),
                              stations = XElementToList_BusLineStation(b.Element("stations"))
                          }
                        ).FirstOrDefault();

            if (_b == null)
                throw new KeyNotExistExeption();

            return _b;
        }

        public IEnumerable<object> GetBusLinesKeys(Func<int, object> generate)
        {
            XElement busLinesRootElem = XMLTools.LoadListFromXMLElement(busLinesPath);

            return from l in busLinesRootElem.Elements()
                   select generate(Int32.Parse(l.Element("key").Value));
        }

        public void UpdateBusLine(int ln, Action<BusLine> update)
        {
            XElement busLinesRootElem = XMLTools.LoadListFromXMLElement(busLinesPath);

            BusLine _b = (from b in busLinesRootElem.Elements()
                          where int.Parse(b.Element("key").Value) == ln
                          select new BusLine()
                          {
                              key = Int32.Parse(b.Element("key").Value),
                              area = (Area)Enum.Parse(typeof(Area), b.Element("area").Value),
                              stations = XElementToList_BusLineStation(b.Element("stations"))
                          }
                        ).FirstOrDefault();

            if (_b != null)
            {
                DeleteBusLine(_b.key);
                update(_b);
                AddBusLine(_b);
            }
            else
                throw new LnNotExistExeption($"bad License num: {ln}");
        }
        public void DeleteBusLine(int key)
        {
            XElement busLinesRootElem = XMLTools.LoadListFromXMLElement(busLinesPath);

            foreach (XElement xe in busLinesRootElem.Elements())
            {
                if (key == int.Parse(xe.Element("key").Value))
                    xe.Remove();
            }

            XMLTools.SaveListToXMLElement(busLinesRootElem, busLinesPath);
        }

        public void ClearBusLines()
        {
            XElement busLinesRootElem = new XElement("BusLines");
            XMLTools.SaveListToXMLElement(busLinesRootElem, busLinesPath);
        }

        public void AddBusLine(BusLine b)
        {
            XElement busLinesRootElem;
            try
            {
                busLinesRootElem = XMLTools.LoadListFromXMLElement(busLinesPath);
            }
            catch(XMLFileLoadCreateException)
            {
                busLinesRootElem = new XElement("BusLinesList");
            }
            XElement bus1 = (from p in busLinesRootElem.Elements()
                             where int.Parse(p.Element("key").Value) == b.key
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new BadUserIdException(b.key, "Duplicate bus key");

            XElement busLineElem = new XElement("BusLine",
                new XElement("key", b.key),
                new XElement("area", b.area),
                ListToXElement_BusLineStation(b.stations));

            busLinesRootElem.Add(busLineElem);

            XMLTools.SaveListToXMLElement(busLinesRootElem, busLinesPath);
        }
        #endregion

        //XMLSerializer
        #region Line Trip
        public LineTrip GetLineTrip(int key)
        {
            List<LineTrip> ListTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripsPath);

            LineTrip t = ListTrips.Find(p => p.LineKey == key);
            if (t != null)
                return t; //no need to Clone()
            else
                throw new KeyNotExistExeption($"bad BusLine key: {key}");
        }

        public void DeleteLineTrip(int key)
        {
            List<LineTrip> ListTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripsPath);

            LineTrip t = ListTrips.Find(p => p.LineKey == key);

            if (t != null)
            {
                ListTrips.Remove(t);
            }
            else
                throw new KeyNotExistExeption($"bad BusLine key: {key}");

            XMLTools.SaveListToXMLSerializer(ListTrips, lineTripsPath);
        }

        public void ClearLineTrips()
        {
            List<LineTrip> ListTrips = new List<LineTrip>();
            XMLTools.SaveListToXMLSerializer(ListTrips, lineTripsPath);
        }

        public void AddLineTrip(LineTrip lt)
        {
            List<LineTrip> ListTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripsPath);

            if (ListTrips.FirstOrDefault(p => p.LineKey == lt.LineKey) != null)
                throw new BadBusLicenseNumException("Duplicate LineTrip Key");

            ListTrips.Add(lt); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListTrips, lineTripsPath);
        }
        #endregion
    }
}
