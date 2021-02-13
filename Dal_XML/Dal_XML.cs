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
        
        string busesPath = @"BusesXml.xml"; //XMLSerializer
        string coursesPath = @"CoursesXml.xml"; //XMLSerializer
        string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
        string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
        string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer
        #endregion

        // XElement
        #region User
        public void AddUser(User user)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            XElement per1 = (from p in usersRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == user.ID
                             select p).FirstOrDefault();

            if (per1 != null)
                throw new BadUserIdException(user.ID, "Duplicate person ID");

            XElement personElem = new XElement("Person",
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
            throw new NotImplementedException();
        }
        #endregion

        #region Station
        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetStationsKeys(Func<int, object> generate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int key, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus Line
        public BusLine GetBusLine(int key)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetBusLinesKeys(Func<int, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int ln, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusLine(int key)
        {
            throw new NotImplementedException();
        }

        public void ClearBusLines()
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BusLine b)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Line Trip
        public LineTrip GetLineTrip(int key)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int key)
        {
            throw new NotImplementedException();
        }

        public void ClearLineTrips()
        {
            throw new NotImplementedException();
        }

        public void AddLineTrip(LineTrip lt)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
