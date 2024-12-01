namespace Queue
{
    public class CircularQueue<T> : IQueue<T>
    {
        private T[] _items;
        private int _head;
        private int _tail;
        private int _count;
        private const int _defaultCapacity = 4;

        public CircularQueue()
        {
            _items = new T[_defaultCapacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                throw new InvalidOperationException("Queue is full.");
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            T item = _items[_head];
            _head = (_head + 1) % _items.Length;
            _count--;
            return item;
        }

        public bool IsEmpty() => _count == 0;

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            return _items[_head];
        }
    }
}
