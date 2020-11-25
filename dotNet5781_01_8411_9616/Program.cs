using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8411_9616
{
    class Program
    {
        static Random r;
        
        enum CHOICE //The purpose of this enum is to limit the values for a users 
                    // choice, and, to give the choices an expressive name.
            { ADD = 1, DRIVE = 2, FIX = 3, DISPLAY = 4, EXIT = 5, INVALID = -1 }
        
        static CHOICE inputChoice()
            //This method encapsulates the process of reading input from the console 
            //and converting it into the 'CHOICE' enum type for convinience.
        {
            int input;
            bool succ = Int32.TryParse(Console.ReadLine(), out input);
            if (succ)
            {
                switch (input)
                {
                    case (int)CHOICE.ADD:
                        return CHOICE.ADD;

                    case (int)CHOICE.DRIVE:
                        return CHOICE.DRIVE;

                    case (int)CHOICE.FIX:
                        return CHOICE.FIX;

                    case (int)CHOICE.DISPLAY:
                        return CHOICE.DISPLAY;

                    case (int)CHOICE.EXIT:
                        return CHOICE.EXIT;

                    default:
                        return CHOICE.INVALID;

                }
            }
            else
                return CHOICE.INVALID;
        }

        //The following four methods perform their respecive operations on
        // buses inclding all the necessary input & output.
        //Some take in the list of buses as reference in order to update it.
        private static void AddBus(ref List<Bus> buses)
        {
            DateTime startDate = new DateTime();

            bool success = false;
            while(!success)
            {
                Console.WriteLine("Enter the date of the start of operation of this bus (dd/mm/yyyy):");
                success = DateTime.TryParse(Console.ReadLine(), out startDate);
                if (!success)
                    Console.WriteLine("Invalid input, try again:");
            }


            success = false;
            string license = "0";// = 0, numDigits = 0;
            
            while(!success)
            {
                Console.WriteLine("\nEnter license plate number:");
                //success = Int32.TryParse(Console.ReadLine(), out license);
                license = Console.ReadLine();
                success = true;
                foreach (char ch in license)
                {
                    if (ch < '0' || ch > '9')
                        success = false;
                }

                if (!success)
                    Console.WriteLine("Invalide input, try again:");
                else
                {
                    string nLicense = Bus.MakeLicenseNum(license, startDate);
                    foreach (Bus b in buses)
                        if (b.GetLicenseNum() == nLicense)
                        {
                            Console.WriteLine("Bus with this license already exists!, try again:");
                            success = false;
                        }
                }
            }


            Bus bus = new Bus(license, startDate);
            buses.Add(bus);
        }
        
        private static void DriveBus(ref List<Bus> buses)
        {
            bool success = false;
            string license = "0";
            while (!success)
            {
                Console.WriteLine("Enter license plate number:");
                success = true;

                license = Console.ReadLine();
                foreach (char ch in license)
                {
                    if (ch < '0' || ch > '9')
                        success = false;
                }
                // success = Int32.TryParse(Console.ReadLine(), out license);
                
                if (!success)
                    Console.WriteLine("Invalide input, try again:");
            }

            //Now check it exists:
            success = false;

            int busIdx = 0;

            while (busIdx < buses.Count() && !success)
            {
                if (buses[busIdx].GetLicenseNum() == Bus.MakeLicenseNum(license))
                    success = true;
                else
                    busIdx++;
            }
            if (!success)
            {
                Console.WriteLine("License plate doesnt exist in database, try again:");
                return;
            }

            int distance = r.Next(0, 10);
            if (buses[busIdx].CanDrive(distance))
                buses[busIdx].Drive(distance);
            else
                Console.WriteLine("This bus is unable to drive requested distance.");
        }
        
        private static void FixBus(ref List<Bus> buses)
        {
            bool success = false;
            string license = "0";
            while (!success)
            {
                Console.WriteLine("Enter license plate number:");
                success = true;

                license = Console.ReadLine();
                foreach (char ch in license)
                {
                    if (ch < '0' || ch > '9')
                        success = false;
                }
                // success = Int32.TryParse(Console.ReadLine(), out license);

                if (!success)
                    Console.WriteLine("Invalide input, try again:");
            }

            //Now check it exists:
            success = false;

            int busIdx = 0;

            while (busIdx < buses.Count() && !success)
            {
                if (buses[busIdx].GetLicenseNum() == Bus.MakeLicenseNum(license))
                    success = true;
                else
                    busIdx++;
            }
            if (!success)
            {
                Console.WriteLine("License plate doesnt exist in database, try again:");
                return;
            }

            bool choice; //True if Repair, false if refuel.
            success = false;
            do
            {
                Console.WriteLine("Enter 'false' for refueling, 'true' for repair:");
                success = bool.TryParse(Console.ReadLine(), out choice);
                if (!success)
                    Console.WriteLine("Invalid input, try again:");
            } while (!success);

            if(choice)//Repair
            {
                buses[busIdx].Service();
            }
            else//Refuel
            {
                buses[busIdx].Refuling();
            }
        }
        
        private static void DisplayBus(List<Bus> buses)
        {
            foreach (Bus bus in buses)
                Console.WriteLine("License: " + bus.GetLicenseNum() +
                    ",\tmilage: " + bus.GetMileage_Km() + 
                    ",\tfuel: " + bus.GetFuel()
                    + '\n');         
        }

        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus> ();

            CHOICE choice;
            
            const string options = "\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "Choose from the following:\n" +
                             "1 - Add a new buss.\n" +
                             "2 - Drive an existing bus.\n" +
                             "3 - Fix a bus.\n" +
                             "4 - Display a buses driving information since last fix.\n" +
                             "5 - Exit.\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

            r = new Random(DateTime.Now.Millisecond);

            do
            {
                Console.WriteLine(options);
                choice = inputChoice();
                switch (choice)
                {
                    case CHOICE.ADD:
                        AddBus(ref buses);
                        break;
                    case CHOICE.DRIVE:
                        DriveBus(ref buses);
                        break;
                    case CHOICE.FIX:
                        FixBus(ref buses);
                        break;
                    case CHOICE.DISPLAY:
                        DisplayBus(buses);
                        break;
                    case CHOICE.EXIT:
                        break;
                    case CHOICE.INVALID:
                    default:
                        Console.WriteLine("Invalid input, please try again:");
                        break;
                }

            } while (choice != CHOICE.EXIT);

        }
    }
}
