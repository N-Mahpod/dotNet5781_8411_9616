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

        }
    }
}
