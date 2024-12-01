namespace Queue
{
    public class LinkedListQueue<T> : IQueue<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public void Enqueue(T item)
        {
            _list.AddLast(item);
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            var value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        public bool IsEmpty() => _list.Count == 0;

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty.");

            return _list.First.Value;
        }
    }
}
