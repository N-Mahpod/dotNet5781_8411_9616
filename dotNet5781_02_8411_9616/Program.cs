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
        }
    }
}
