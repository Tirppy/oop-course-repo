namespace Queue
{
    public class ArrayQueue<T> : IQueue<T>
    {
        private T[] _items;
        private int _count;
        private const int _defaultCapacity = 4;

        public ArrayQueue()
        {
            _items = new T[_defaultCapacity];
            _count = 0;
        }

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _count * 2);
            }
            _items[_count++] = item;
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            T item = _items[0];
            Array.Copy(_items, 1, _items, 0, _count - 1);
            _count--;
            return item;
        }

        public bool IsEmpty() => _count == 0;

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            return _items[0];
        }
    }
}
