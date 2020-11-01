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
                        break;
                    case (int)CHOICE.DRIVE:
                        return CHOICE.DRIVE;
                        break;
                    case (int)CHOICE.FIX:
                        return CHOICE.FIX;
                        break;
                    case (int)CHOICE.DISPLAY:
                        return CHOICE.DISPLAY;
                        break;
                    case (int)CHOICE.EXIT:
                        return CHOICE.EXIT;
                        break;
                    default:
                        return CHOICE.INVALID;
                       break;
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
            bool success = false;
            int license = 0, numDigits = 0;
            while(!success)
            {
                Console.WriteLine("Enter license plate number:\n");
                success = Int32.TryParse(Console.ReadLine(), out license);
                if (!success)
                    Console.WriteLine("Invalide input, try again:\n");
                else
                    foreach (Bus b in buses)
                        if (b.GetLicenseNum().GetNumber() == license)
                        {
                            Console.WriteLine("Bus with this license already exists!, try again:\n");
                            success = false;
                        }
            }

            DateTime startDate = new DateTime();

            success = false;
            while(!success)
            {
                Console.WriteLine("Enter the date of the start of operation of this bus (dd/mm/yyyy):\n");
                success = DateTime.TryParse(Console.ReadLine(), out startDate);
                if (!success)
                    Console.WriteLine("Invalid input, try again:\n");
            }

            Bus bus = new Bus(license, startDate);
            buses.Add(bus);
        }
        private static void DriveBus(ref List<Bus> buses)
        {
            bool success = false;
            int license = 0;
            while (!success)
            {
                Console.WriteLine("Enter license plate number:\n");
                success = Int32.TryParse(Console.ReadLine(), out license);
                if (!success)
                    Console.WriteLine("Invalide input, try again:\n");
            }

            //Now check it exists:
            success = false;

            int busIdx = 0;

            while (busIdx < buses.Count() && !success)
            {
                if (buses[busIdx].GetLicenseNum().GetNumber() == license)
                    success = true;
                else
                    busIdx++;
            }
            if (!success)
            {
                Console.WriteLine("License plate doesnt exist in database, try again:\n");
                return;
            }

            int distance = r.Next(0, 10);
            if (buses[busIdx].CanDrive(distance))
                buses[busIdx].Drive(distance);
            else
                Console.WriteLine("This bus is unable to drive requested distance.\n");
        }
        private static void FixBus(ref List<Bus> buses)
        {
            bool success = false;
            int license = 0;
            while (!success)
            {
                Console.WriteLine("Enter license plate number:\n");
                success = Int32.TryParse(Console.ReadLine(), out license);
                if (!success)
                    Console.WriteLine("Invalide input, try again:\n");
            }

            //Now check it exists:
            success = false;

            int busIdx = 0;

            while (busIdx < buses.Count() && !success)
            {
                if (buses[busIdx].GetLicenseNum().GetNumber() == license)
                    success = true;
                else
                    busIdx++;
            }
            if (!success)
            {
                Console.WriteLine("License plate doesnt exist in database, try again:\n");
                return;
            }

            bool choice; //True if Repair, false if refuel.
            success = false;
            do
            {
                Console.WriteLine("Enter 0 for refueling, 1 for repair:\n");
                success = bool.TryParse(Console.ReadLine(), out choice);
                if (!success)
                    Console.WriteLine("Invalid input, try again:\n");
            } while (!success);

            if(choice)//Repair
            {
                buses[busIdx].Service(DateTime.Now.Date);
            }
            else//Refuel
            {
                buses[busIdx].Refuling();
            }
        }
        private static void DisplayBus(List<Bus> buses)
        {
            foreach (Bus bus in buses)
                Console.WriteLine("License: " + bus.GetLicenseNum().GetStringNumber() +
                    ", milage: " + bus.GetMileage_Km() + '\n');         
        }

        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus> ();

            CHOICE choice;
            
            const string options = "Choose from the following:\n" +
                             "Add a new buss - 1\n" +
                             "Drive an existing bus - 2\n" +
                             "Fix a bus - 3\n" +
                             "Display a buses driving information since last fix - 4\n" +
                             "Exit - 5\n";

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
                        Console.WriteLine("Invalid input, please try again:\n");
                        break;
                }

            } while (choice != CHOICE.EXIT);

        }
    }
}
