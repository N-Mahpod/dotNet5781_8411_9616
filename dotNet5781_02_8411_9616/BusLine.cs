using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    class BusLineStation : BusStation
    {
        //Distance from previous and next station in this line, respectively.
        private double distPrev;
        private double distNext;

        //Getters for distances.
        public double DistPrev { get => distPrev; }
        public double DistNext { get => distNext; }

        //Utility function to calculate distance between this station and another.
        private double getDistance(in BusLineStation other)
        {
            return Math.Sqrt(Math.Pow(longitude - other.Longitude, 2) + Math.Pow(latitude - other.Latitude, 2));
        }

        public BusLineStation(in BusLineStation prev, in BusLineStation next, in string adress = "")
            : base(adress)
        {
            if(prev != null) 
                distPrev = getDistance(prev);
           
            if(next != null)
                distNext = getDistance(next);
        }
    }

    class BusLine
    {
        //The line ID.
        private int id;
        //Starting and finishing stations respectively.
        private BusLineStation start;
        private BusLineStation finish;

    }
}
