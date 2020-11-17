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

            
            /* //~~~~~~~~~~~~~~~~~~~~~~~> CHECK <~~~~~~~~~~~~~~~~~~~~~~~~~
             * List<BusStation> l = new List<BusStation>();
             * Console.WriteLine("start (if it is'nt working, try to enter five adresses)");
             * for (int i = 0; i < 5; i++)
             * {
             *     string s = Console.ReadLine();
             *     BusStation b = new BusStation(s);
             *     l.Add(b);
             * }
             * for (int i = 0; i < 5; i++)
             * {
             *     Console.WriteLine(i.ToString() + ":\t" + l[i].ToString());
             * }
             * Console.ReadKey();
             * 
             * Console.WriteLine("next level:\n");
             * 
             * BusLine busLine = new BusLine(5, 3);
             * BusLineStation bls = new BusLineStation(l[0]);
             * busLine.Add(bls);
             * for (int i = 1; i < 5; i++)
             * {
             *     bls = new BusLineStation(l[i], l[i].getDistance(l[i - 1]), i * 10);
             *     busLine.Add(bls);
             * }
             * 
             * Console.WriteLine("OK:\n" + busLine.ToString());
             * Console.ReadKey();
             * 
             * Console.WriteLine("next level:\n");
             * Console.WriteLine("Enter a lot of adresses");
             * 
             * LinesCollection h = new LinesCollection();
             * for (int i = 0; i < 5; i++)
             * {
             *     BusLine bl = new BusLine(i, i);
             *     for (int j = 0; j < 5; j++)
             *     {
             *         string s = Console.ReadLine();
             *         BusStation bs = new BusStation(s);
             *         BusLineStation bls2;
             *         if (j == 0)
             *             bls2 = new BusLineStation((BusStation)bs);
             *         else
             *             bls2 = new BusLineStation(true, bl[j - 1], i * j * 35);
             *             // Here there was an absolutly stupid error that I didn't understand why.
             *             // The compiler chose the wrong constructor from some unknown reason,
             *             // and even when I tried to add a bool flag to the constructor it didn't work,
             *             // and I have no idea why. So anoyng.
             *         bl.Add(bls2);
             *     }
             *     h.Add(bl);
             * }
             * Console.WriteLine(h.ToString());
             * Console.ReadKey();
             */
        }
    }
}
