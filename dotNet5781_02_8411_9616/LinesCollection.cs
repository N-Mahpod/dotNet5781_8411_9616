using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8411_9616
{
    class LinesCollection : IEnumerable
    {
        private List<BusLine> collection;

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


        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public LinesEnum GetEnumerator()
        {
            return new LinesEnum(collection);
        }
    }

    class LinesEnum : IEnumerator
    {
        public List<BusLine> _lines;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public LinesEnum(List<BusLine> list)
        {
            _lines = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _lines.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public BusLine Current
        {
            get
            {
                try
                {
                    return _lines[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
