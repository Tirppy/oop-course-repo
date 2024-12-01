namespace CarServiceLab.Tests
{
    [TestClass]
    public class CarStationTests
    {
        [TestMethod]
        public void TestQueueA_EnqueueAndDequeue()
        {
            var station = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA());

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30);
            var car2 = new Car(2, "ELECTRIC", "PEOPLE", false, 45);

            station.AddCar(car1);
            station.AddCar(car2);

            var sw = new StringWriter();
            Console.SetOut(sw);

            station.ServeCars();

            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Serving dinner to people in car 1.")); 
            Assert.IsTrue(output.Contains("Refueling electric car 1.")); 
            Assert.IsFalse(output.Contains("Serving dinner to people in car 2.")); 
            Assert.IsTrue(output.Contains("Refueling electric car 2.")); 
        }

        [TestMethod]
        public void TestQueueB_EnqueueAndDequeue()
        {
            var station = new CarStation(new RobotDinner(), new ElectricStation(), new QueueB());

            var car1 = new Car(1, "ELECTRIC", "ROBOTS", true, 30);
            var car2 = new Car(2, "ELECTRIC", "ROBOTS", false, 45);

            station.AddCar(car1);
            station.AddCar(car2);

            var sw = new StringWriter();
            Console.SetOut(sw);

            station.ServeCars();

            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Serving dinner to robots in car 1.")); 
            Assert.IsTrue(output.Contains("Refueling electric car 1.")); 
            Assert.IsFalse(output.Contains("Serving dinner to robots in car 2.")); 
            Assert.IsTrue(output.Contains("Refueling electric car 2.")); 
        }

        [TestMethod]
        public void TestQueueC_EnqueueAndDequeue()
        {
            var station = new CarStation(new PeopleDinner(), new GasStation(), new QueueC());

            var car1 = new Car(1, "GAS", "PEOPLE", true, 30);
            var car2 = new Car(2, "GAS", "PEOPLE", false, 45);

            station.AddCar(car1);
            station.AddCar(car2);

            var sw = new StringWriter();
            Console.SetOut(sw);

            station.ServeCars();

            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Serving dinner to people in car 1.")); 
            Assert.IsTrue(output.Contains("Refueling gas car 1.")); 
            Assert.IsFalse(output.Contains("Serving dinner to people in car 2.")); 
            Assert.IsTrue(output.Contains("Refueling gas car 2.")); 
        }

        [TestMethod]
        public void TestQueueA_EmptyQueue()
        {
            var station = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA());

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30);

            station.AddCar(car1);

            station.ServeCars();

            var sw = new StringWriter();
            Console.SetOut(sw);
            station.ServeCars(); 
            string output = sw.ToString();

            Assert.IsTrue(string.IsNullOrEmpty(output), "No cars should be served when the queue is empty.");
        }

        [TestMethod]
        public void TestQueueB_EmptyQueue()
        {
            var station = new CarStation(new RobotDinner(), new GasStation(), new QueueB());

            var sw = new StringWriter();
            Console.SetOut(sw);
            station.ServeCars(); 
            string output = sw.ToString();

            Assert.IsTrue(string.IsNullOrEmpty(output), "No cars should be served when the queue is empty.");
        }

        [TestMethod]
        public void TestQueueC_EmptyQueue()
        {
            var station = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueC());

            var sw = new StringWriter();
            Console.SetOut(sw);
            station.ServeCars(); 
            string output = sw.ToString();

            Assert.IsTrue(string.IsNullOrEmpty(output), "No cars should be served when the queue is empty.");
        }
    }
}
