using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_8411_9616
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9616();
            Welcome8411();
            Console.ReadKey();
        }

        static partial void Welcome8411();

        private static void Welcome9616()
        {
            Console.Write("Enter your name:");
            string pumba = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first cosole application!", pumba);
        }
    }
}
