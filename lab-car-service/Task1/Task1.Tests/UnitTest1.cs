namespace Queue.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void TestArrayQueue()
        {
            IQueue<int> queue = new ArrayQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);

            Assert.AreEqual(10, queue.Dequeue());
            Assert.AreEqual(20, queue.Dequeue());
        }

        [TestMethod]
        public void TestLinkedListQueue()
        {
            IQueue<int> queue = new LinkedListQueue<int>();
            queue.Enqueue(30);
            queue.Enqueue(40);

            Assert.AreEqual(30, queue.Dequeue());
            Assert.AreEqual(40, queue.Dequeue());
        }

        [TestMethod]
        public void TestCircularQueue()
        {
            IQueue<int> queue = new CircularQueue<int>();
            queue.Enqueue(50);
            queue.Enqueue(60);

            Assert.AreEqual(50, queue.Dequeue());
            Assert.AreEqual(60, queue.Dequeue());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDequeueEmptyQueue()
        {
            IQueue<int> queue = new ArrayQueue<int>();
            queue.Dequeue();  // Should throw an exception because the queue is empty
        }

        [TestMethod]
        public void TestPeekQueue()
        {
            IQueue<int> queue = new ArrayQueue<int>();
            queue.Enqueue(70);
            queue.Enqueue(80);

            Assert.AreEqual(70, queue.Peek());
        }
    }
}
