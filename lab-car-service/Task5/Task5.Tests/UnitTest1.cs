using System.Text.Json;

namespace CarServiceLab.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private string testQueueFolder;

        [TestInitialize]
        public void SetUp()
        {
            testQueueFolder = Path.Combine(Directory.GetCurrentDirectory(), "TestQueue");
            if (Directory.Exists(testQueueFolder))
            {
                Directory.Delete(testQueueFolder, true);
            }
            Directory.CreateDirectory(testQueueFolder);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (Directory.Exists(testQueueFolder))
            {
                Directory.Delete(testQueueFolder, true);
            }
        }

        [TestMethod]
        public void TestTwoCarsProcessedInSameCycle()
        {
            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var gasRobotStation = new CarStation(new RobotDinner(), new GasStation(), new QueueB(), "GAS", "ROBOTS");
            var stations = new List<CarStation> { electricPeopleStation, gasRobotStation };
            var semaphore = new Semaphore(stations);

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30); 
            var car2 = new Car(2, "GAS", "ROBOTS", false, 50);     

            File.WriteAllText(Path.Combine(testQueueFolder, "car1.json"), JsonSerializer.Serialize(car1));
            File.WriteAllText(Path.Combine(testQueueFolder, "car2.json"), JsonSerializer.Serialize(car2));

            var files = Directory.GetFiles(testQueueFolder, "car*.json")
                                 .OrderBy(f => Path.GetFileName(f))
                                 .ToList();

            int expectedCarId = 1;
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));

                if (carId == expectedCarId)
                {
                    var json = File.ReadAllText(file);
                    var car = JsonSerializer.Deserialize<Car>(json);

                    if (car != null)
                    {
                        semaphore.RouteCar(car);
                        expectedCarId++;
                    }
                }
            }

            electricPeopleStation.ServeCars();
            gasRobotStation.ServeCars();

            Assert.AreEqual(1, electricPeopleStation.CarCount, "Electric People Station should process 1 car.");
            Assert.AreEqual(1, gasRobotStation.CarCount, "Gas Robot Station should process 1 car.");
            Assert.AreEqual(1, electricPeopleStation.DinnerCount, "Electric People Station should serve 1 dinner.");
            Assert.AreEqual(0, gasRobotStation.DinnerCount, "Gas Robot Station should not serve dinner (car isDining=false).");
        }

        [TestMethod]
        public void TestMissingCarFileStopsProcessing()
        {
            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueB(), "GAS", "PEOPLE");
            var stations = new List<CarStation> { electricPeopleStation, gasPeopleStation };
            var semaphore = new Semaphore(stations);

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30); 
            var car3 = new Car(3, "GAS", "PEOPLE", true, 40);      

            File.WriteAllText(Path.Combine(testQueueFolder, "car1.json"), JsonSerializer.Serialize(car1));
            File.WriteAllText(Path.Combine(testQueueFolder, "car3.json"), JsonSerializer.Serialize(car3));

            
            var files = Directory.GetFiles(testQueueFolder, "car*.json")
                                 .OrderBy(f => Path.GetFileName(f))
                                 .ToList();

            int expectedCarId = 1;
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));

                if (carId != expectedCarId)
                {
                    break; 
                }

                var json = File.ReadAllText(file);
                var car = JsonSerializer.Deserialize<Car>(json);

                if (car != null)
                {
                    semaphore.RouteCar(car);
                    expectedCarId++;
                }
            }

            electricPeopleStation.ServeCars();
            gasPeopleStation.ServeCars();

            Assert.AreEqual(1, electricPeopleStation.CarCount, "Electric People Station should process 1 car.");
            Assert.AreEqual(0, gasPeopleStation.CarCount, "Gas People Station should process 0 cars (car2.json missing).");
        }

        [TestMethod]
        public void TestCorrectStationRouting()
        {
            var electricRobotStation = new CarStation(new RobotDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "ROBOTS");
            var gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueB(), "GAS", "PEOPLE");
            var stations = new List<CarStation> { electricRobotStation, gasPeopleStation };
            var semaphore = new Semaphore(stations);

            var car1 = new Car(1, "ELECTRIC", "ROBOTS", true, 25); 
            var car2 = new Car(2, "GAS", "PEOPLE", true, 50);      

            File.WriteAllText(Path.Combine(testQueueFolder, "car1.json"), JsonSerializer.Serialize(car1));
            File.WriteAllText(Path.Combine(testQueueFolder, "car2.json"), JsonSerializer.Serialize(car2));

            var files = Directory.GetFiles(testQueueFolder, "car*.json")
                                 .OrderBy(f => Path.GetFileName(f))
                                 .ToList();

            int expectedCarId = 1;
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));

                if (carId == expectedCarId)
                {
                    var json = File.ReadAllText(file);
                    var car = JsonSerializer.Deserialize<Car>(json);

                    if (car != null)
                    {
                        semaphore.RouteCar(car);
                        expectedCarId++;
                    }
                }
            }

            electricRobotStation.ServeCars();
            gasPeopleStation.ServeCars();

            Assert.AreEqual(1, electricRobotStation.CarCount, "Electric Robot Station should process 1 car.");
            Assert.AreEqual(1, gasPeopleStation.CarCount, "Gas People Station should process 1 car.");
        }
    }
}
