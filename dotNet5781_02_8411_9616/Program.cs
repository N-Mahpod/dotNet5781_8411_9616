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
                    blArr[i].Add(new BusLineStation(POSITION.MIDDLE, bsArr[(4 * i + j) % 40],
                    bsArr[(4 * i + j) % 40].getDistance(bsArr[4 * i + j - 1]), r.Next(10, 50)));

                lc.Add(blArr[i]);
            }

            int choice;
            string options = "\n\n~~~~> MAIN < ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "Addition:\n\tNew line: 1.\n\tNew station in existing line: 2.\n" +
                             "Deletion:\n\tDeleting line: 3.\n\tDeleting station from existing line: 4.\n" +
                             "Searching:\n\tSearch lines that go through specific station: 5.\n\tSearch for direct buses between two stations: 6.\n" +
                             "Printing:\n\tAll existing bus lines: 7.\n\tEvery bus station and the lines its in: 8.\n" +
                             "Exit: 9.\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

            int lineId, key;
            bool exist;
            List<BusLine> lstOfStt;
            do
            {
                Console.WriteLine(options);
                Int32.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter new line`s id:");
                        lineId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter new line`s aerial code (1-general, 2-N, 3-S, 4-center, 5-Jr):");
                        int lineArea = Int32.Parse(Console.ReadLine());

                        if (lineArea < 1 || lineArea > 5)
                        {
                            Console.WriteLine("Error in aerial code, try again:");
                            break;
                        }

                        foreach (BusLine bl in lc)
                            if (bl.ID == lineId)
                            {
                                Console.WriteLine("Line ID already exists, try again:");
                                break;
                            }

                        lc.Add(new BusLine(lineId, lineArea));
                        break;

                    case 2:
                        Console.WriteLine("Enter line`s id:");
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

                        Console.WriteLine("Enter station`s key:");
                        key = Int32.Parse(Console.ReadLine());

                        exist = false;
                        foreach (BusStation bs in bsArr)
                            if (bs.GetBusStationKey() == key)
                            {
                                exist = true;
                                try
                                {
                                    BusLine bl = lc[id];
                                    if (bl.GetSize() == 0)
                                        bl.Add(new BusLineStation(POSITION.FIRST, bs));
                                    else
                                    {
                                        Console.WriteLine("Enter drive time from the last station (in minutes):");
                                        double driveTime = double.Parse(Console.ReadLine());
                                        bl.Add(new BusLineStation(POSITION.LAST, bs, bs.getDistance(bl.Finish), driveTime));
                                    }
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine(e.Data);
                                    break;
                                }
                            }
                        if (!exist)
                            Console.WriteLine("Station doesnt exist, try again:");
                        break;

                    case 3:
                        Console.WriteLine("Enter line`s id:");
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
                        Console.WriteLine("Enter line`s id:");
                        lineId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter station`s key:");
                        key = Int32.Parse(Console.ReadLine());

                        try
                        {
                            BusLine bl = lc[lineId];

                            exist = false;

                            foreach (BusLineStation bls in bl.Stations)
                                if (bls.GetBusStationKey() == key)
                                {
                                    exist = true;
                                    bl.Remove(bls);
                                    break;
                                }
                            if (!exist)
                                Console.WriteLine("Sation doesnt exist in line, try again:");
                            break;

                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Data);
                        }
                        break;

                    case 5:
                        Console.WriteLine("Enter station`s key:");
                        key = Int32.Parse(Console.ReadLine());
                        lstOfStt = lc.LinesOfStation(key);

                        if (lstOfStt.Count == 0)
                            Console.WriteLine("This station does'nt include any lines:( try again.");
                        else
                        {
                            foreach (BusLine blh in lstOfStt)
                            {
                                Console.WriteLine(blh.ToString());
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("Enter first station`s key:");
                        int key1 = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter last station`s key:");
                        int key2 = Int32.Parse(Console.ReadLine());

                        LinesCollection lch = new LinesCollection();
                        lstOfStt = lc.LinesOfStation(key1);
                        if (lstOfStt.Count == 0)
                            Console.WriteLine("The first station does'nt include any lines:( try again.");
                        else
                        {
                            foreach (BusLine blh in lstOfStt)
                            {
                                if (blh.IsInclude(key2))
                                {
                                    lch.Add(blh.SubRoute(blh.Stations[blh.FindStation(key1)], blh.Stations[blh.FindStation(key2)]));
                                }
                            }
                            lch.SortByTime();
                            Console.WriteLine("Your options:");
                            for (int i = 0; i < lch.Lines.Count; ++i)
                            {
                                Console.WriteLine((i + 1).ToString() + ": " + lch.Lines[i].ID.ToString());
                                Console.WriteLine("\tTime: " + lch.Lines[i].MinutesBetween(lch.Lines[i].Stations[lch.Lines[i].FindStation(key1)], lch.Lines[i].Stations[lch.Lines[i].FindStation(key2)]));
                            }
                            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~> you are here!
                        }
                        break;

                    case 7:
                        Console.WriteLine("Lines:");
                        Console.WriteLine(lc.ToString());
                        break;

                    case 8:
                        Console.WriteLine("Stations:");
                        for (int i = 0; i < bsArr.Length; ++i)
                        {
                            Console.WriteLine(bsArr[i].ToString());
                            lstOfStt = lc.LinesOfStation(bsArr[i].GetBusStationKey());
                            Console.Write("Lines:\t");
                            for (int j = 0; j < lstOfStt.Count; ++j)
                            {
                                Console.Write((j + 1).ToString() + ": " + lstOfStt[j].ID.ToString() + "\t");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 9:
                        break;

                    default:
                        Console.WriteLine("Not an option, try again:");
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