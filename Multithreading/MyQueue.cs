using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class MyQueue<T>
    {
        private List<T> _items;
        private int index;

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public T Last
        {
            get
            {
                return _items[index];
            }
        }

        public T First
        {
            get
            {
                return _items[0];
            }
        }

        public MyQueue()
        {
            index = -1;
            _items = new List<T>();
        }

        public void Enqueue(T item)
        {
            _items.Add(item);
            index++;
        }

        public T Dequeue()
        {
            T i = _items[0];
            _items.RemoveAt(0);
            index--;
            return i;
        }

        public T Peek()
        {
            return _items[0];
        }
    }
}
