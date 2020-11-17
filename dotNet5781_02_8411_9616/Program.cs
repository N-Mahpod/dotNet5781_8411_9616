using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    class Program
    {
        static void Main(string[] args)
        {

            Random r = new Random();

            string[] addresses = new string[40]//Need to put here 40 names of fictional bus station addresses
            {
            "","","","","",
            "","","","","",
            "","","","","",
            "","","","","",
            "","","","","",
            "","","","","",
            "","","","","",
            "","","","","",
            };

            BusStation[] bsArr = new BusStation[40];
            for (var i = 0; i < 40; ++i) 
            {
                bsArr[i] = 
                    new BusStation(addresses[i]);
            }

            LinesCollection lc = new LinesCollection();

            BusLine[] blArr = new BusLine[10];
            for (var i = 0; i < 10; ++i) 
            {
                blArr[i] = new BusLine(i + 1, r.Next(1, 5));
                blArr[i].Add(new BusLineStation(POSITION.FIRST, bsArr[4 * i]));

                for (var j = 1; j <= 4; j++)
                    blArr[i].Add(new BusLineStation(POSITION.MIDDLE, bsArr[4 * i + j],
                    bsArr[4 * i + j].getDistance(bsArr[4 * i + j - 1]), r.Next(10, 50)));

                lc.Add(blArr[i]);
            }

            int choice;
            string options = "Addition:\n\tNew line: 1.\n\tNew bus in existing line: 2.\n" +
                             "Deletion:\n\tDeleting line: 3.\n\tDeleting station from existing line: 4.\n" +
                             "Searching:\n\tSearch lines that go through specific station: 5.\n\tSearch for direct buses between two stations: 6.\n" +
                             "Printing:\n\tAll existing bus lines: 7.\n\tEvery bus station and the lines its in: 8.\n" +
                             "Exit: 9.\n";
            int lineId, key;
            do
            {
                Console.WriteLine(options);
                Int32.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                    {
                        case 1:
                        Console.WriteLine("Enter new line`s id:\n");
                        lineId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter new line`s aerial code (1-general, 2-N, 3-S, 4-center, 5-Jr):\n");
                        int lineArea = Int32.Parse(Console.ReadLine());

                        if (lineArea < 1 || lineArea > 5)
                        {
                            Console.WriteLine("Error in aerial code, try again:\n");
                            break;
                        }

                        foreach(BusLine bl in lc)
                            if(bl.ID == lineId)
                            {
                                Console.WriteLine("Line ID already exists, try again:\n");
                                break;
                            }

                        lc.Add(new BusLine(lineId, lineArea));
                        break;

                    case 2:
                        Console.WriteLine("Enter line`s id:\n");
                        int id = Int32.Parse(Console.ReadLine());
                        
                        /*int i = 0;
                        bool found = false;
                        foreach(BusLine bl in lc)
                        {
                            if (bl.ID == id)
                            {
                                found = true;
                                break;
                            }
                            else
                                i++;
                        }
                        
                        if(found == false)
                        {
                            Console.WriteLine("Line doesnt exist, try again:\n");
                            break;
                        }*/

                        Console.WriteLine("Enter station`s key:\n");
                        key = Int32.Parse(Console.ReadLine());

                        foreach(BusStation bs in bsArr)
                            if(bs.GetBusStationKey() == key)
                            {
                                try
                                {
                                    BusLine bl = lc[id];
                                    if (bl.GetSize() == 0)
                                        bl.Add(new BusLineStation(POSITION.FIRST, bs));
                                    else
                                        bl.Add(new BusLineStation(POSITION.LAST, bs,
                                            bs.getDistance(bl.Finish)));

                                }catch(IndexOutOfRangeException e)
                                {
                                    Console.WriteLine(e.Data);
                                    break;
                                }
                            }

                        Console.WriteLine("Station doesnt exist, try again:\n");
                        break;

                    case 3:
                        Console.WriteLine("Enter line`s id:\n");
                        lineId = Int32.Parse(Console.ReadLine());

                        try
                        {
                            BusLine bl = lc[lineId];
                            lc.Remove(bl);
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Data);
                            break;
                        }
                        break;

                    case 4:
                        Console.WriteLine("Enter line`s id:\n");
                        lineId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter station`s key:\n");
                        key = Int32.Parse(Console.ReadLine());

                        try
                        {
                            
                            BusLine bl = lc[lineId];

                            foreach(BusLineStation bls in bl.Stations)
                                if(bls.GetBusStationKey() == key)
                                {
                                    bl.Remove(bls);
                                    break;
                                }

                            Console.WriteLine("Sation doesnt exist in line, try again:\n");
                            break;

                        }
                        catch(IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Data);
                        }
                        break;

                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    default:
                        Console.WriteLine("Not an option, try again:\n");
                        break;
                }
            } while (choice != 9);
        }
    }
}

            /*
            List<BusStation> l = new List<BusStation>();
            Console.WriteLine("start (if it is'nt working, try to tap five adresses)");
            for (int i = 0; i < 5; i++)
            {
                string s = Console.ReadLine();
                BusStation b = new BusStation(s);
                l.Add(b);
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i.ToString() + ":\t" + l[i].ToString());
            }
            Console.ReadKey();

            Console.WriteLine("next level:\n");

            BusLine busLine = new BusLine(5, 3);
            BusLineStation bls = new BusLineStation(l[0]);
            busLine.Add(bls);
            for (int i = 1; i < 5; i++)
            {
                bls = new BusLineStation(l[i], l[i].getDistance(l[i - 1]), i * 10);
                busLine.Add(bls);
            }

            Console.WriteLine("OK:\n" + busLine.ToString());
            Console.ReadKey();
            */

            /*
            Console.WriteLine("next level:\n");
            Console.WriteLine("Enter a lot of adresses");

            LinesCollection h = new LinesCollection();
            for (int i = 0; i < 5; i++)
            {
                BusLine bl = new BusLine(i, i);
                for (int j = 0; j < 5; j++)
                {
                    string s = Console.ReadLine();
                    BusStation bs = new BusStation(s);
                    BusLineStation bls2;
                    if (j == 0)
                        bls2 = new BusLineStation(bs);
                    else
                        bls2 = new BusLineStation(true, bl[j - 1], i * j * 35);
                    bl.Add(bls2);
                }
                h.Add(bl);
            }
            Console.WriteLine(h.ToString());
            Console.ReadKey();
            */