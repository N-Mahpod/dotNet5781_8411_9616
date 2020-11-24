using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    public class LinesCollection : IEnumerable
    {
        private List<BusLine> collection;


        public List<BusLine> Lines
        {
            get => collection;
        }
        public LinesCollection()
        {
            collection = new List<BusLine>();
        }

        public void Add(BusLine line)
        {
            foreach (BusLine bus in collection)
            {
                if ((bus.ID == line.ID) && ((bus.Start != line.Finish) || (bus.Finish != line.Start)))
                    return;
            }

            collection.Add(line);
        }

        public void Remove(BusLine line)
        {
            collection.Remove(line);
        }

        public BusLine this[int i]
        {
            get
            {
                foreach (BusLine line in collection)
                {
                    if (line.ID == i)
                        return line;
                }

                throw new IndexOutOfRangeException("This line number doesn't exsist in this collection :-/");
            }
        }

        public List<BusLine> LinesOfStation(int StatNum)
        {
            List<BusLine> lines = new List<BusLine>();
            foreach (BusLine b in collection)
            {
                if (b.IsInclude(StatNum))
                    lines.Add(b);
            }
            return lines;
        }

        public void SortByTime()
        {
            collection.Sort();
            collection.Reverse();
        }

        public override string ToString()
        {
            string s = "";
            foreach (BusLine b in collection)
            {
                s += b.ToString();
                s += "\n";
            }
            return s;
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}