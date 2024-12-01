namespace CarServiceLab
{
    public interface ICarQueue
    {
        void Enqueue(Car car);
        Car Dequeue();
        int Count { get; }
    }

    public class QueueA : ICarQueue
    {
        private LinkedList<Car> _queue = new LinkedList<Car>();

        public void Enqueue(Car car)
        {
            _queue.AddLast(car);
        }

        public Car Dequeue()
        {
            if (_queue.Count > 0)
            {
                var firstCar = _queue.First.Value;
                _queue.RemoveFirst();
                return firstCar;
            }
            throw new InvalidOperationException("Queue is empty");
        }

        public int Count => _queue.Count;
    }

    public class QueueB : ICarQueue
    {
        private Stack<Car> _stack1 = new Stack<Car>();
        private Stack<Car> _stack2 = new Stack<Car>();

        public void Enqueue(Car car)
        {
            _stack1.Push(car);
        }

        public Car Dequeue()
        {
            if (_stack2.Count == 0)
            {
                while (_stack1.Count > 0)
                {
                    _stack2.Push(_stack1.Pop());
                }
            }

            if (_stack2.Count > 0)
            {
                return _stack2.Pop();
            }
            throw new InvalidOperationException("Queue is empty");
        }

        public int Count => _stack1.Count + _stack2.Count;
    }

    public class QueueC : ICarQueue
    {
        private List<Car> _queue = new List<Car>();

        public void Enqueue(Car car)
        {
            _queue.Add(car);
        }

        public Car Dequeue()
        {
            if (_queue.Count > 0)
            {
                var firstCar = _queue[0];
                _queue.RemoveAt(0);
                return firstCar;
            }
            throw new InvalidOperationException("Queue is empty");
        }

        public int Count => _queue.Count;
    }
}