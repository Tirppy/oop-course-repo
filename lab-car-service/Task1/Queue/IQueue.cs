namespace Queue
{
    public interface IQueue<T>
    {
        void Enqueue(T item);
        T Dequeue();
        bool IsEmpty();
        T Peek();
    }
}
