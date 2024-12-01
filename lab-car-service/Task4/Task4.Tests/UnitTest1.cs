using System.Text.Json;

namespace CarServiceLab.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private string testDirectory;

        [TestInitialize]
        public void SetUp()
        {
            testDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..", "testQueue");
            Directory.CreateDirectory(testDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (Directory.Exists(testDirectory))
            {
                var files = Directory.GetFiles(testDirectory);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(testDirectory);
            }
        }

        [TestMethod]
        public void TestCarProcessingAndStationStats()
        {
            string carJson1 = "{\"id\": 1, \"type\": \"GAS\", \"passengers\": \"PEOPLE\", \"isDining\": true, \"consumption\": 30}";
            string carJson2 = "{\"id\": 2, \"type\": \"ELECTRIC\", \"passengers\": \"ROBOTS\", \"isDining\": false, \"consumption\": 40}";

            File.WriteAllText(Path.Combine(testDirectory, "car1.json"), carJson1);
            File.WriteAllText(Path.Combine(testDirectory, "car2.json"), carJson2);

            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var electricRobotStation = new CarStation(new RobotDinner(), new ElectricStation(), new QueueB(), "ELECTRIC", "ROBOTS");
            var gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueC(), "GAS", "PEOPLE");
            var gasRobotStation = new CarStation(new RobotDinner(), new GasStation(), new QueueA(), "GAS", "ROBOTS");

            var stations = new List<CarStation> { electricPeopleStation, electricRobotStation, gasPeopleStation, gasRobotStation };
            var semaphore = new Semaphore(stations);

            string queueFolder = testDirectory;
            var files = Directory.GetFiles(queueFolder, "car*.json").OrderBy(f => Path.GetFileName(f)).ToList();
            int expectedCarId = 1;

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));
                if (carId != expectedCarId)
                {
                    break; 
                }

                string fileContent = File.ReadAllText(file);
                var car = JsonSerializer.Deserialize<Car>(fileContent);

                if (car != null)
                {
                    semaphore.RouteCar(car);
                }

                expectedCarId++;
            }

            electricPeopleStation.ServeCars();
            electricRobotStation.ServeCars();
            gasPeopleStation.ServeCars();
            gasRobotStation.ServeCars();

            Assert.AreEqual(0, electricPeopleStation.CarCount);
            Assert.AreEqual(1, electricRobotStation.CarCount);
            Assert.AreEqual(1, gasPeopleStation.CarCount);
            Assert.AreEqual(0, gasRobotStation.CarCount);

            Assert.AreEqual(0, electricPeopleStation.DinnerCount);
            Assert.AreEqual(0, electricRobotStation.DinnerCount);
            Assert.AreEqual(1, gasPeopleStation.DinnerCount);
            Assert.AreEqual(0, gasRobotStation.DinnerCount);

            Assert.AreEqual(30, gasPeopleStation.TotalConsumption);
            Assert.AreEqual(0, electricPeopleStation.TotalConsumption);
            Assert.AreEqual(40, electricRobotStation.TotalConsumption);
            Assert.AreEqual(0, gasRobotStation.TotalConsumption);

            int totalCars = electricPeopleStation.CarCount + electricRobotStation.CarCount + gasPeopleStation.CarCount + gasRobotStation.CarCount;
            int totalDinners = electricPeopleStation.DinnerCount + electricRobotStation.DinnerCount + gasPeopleStation.DinnerCount + gasRobotStation.DinnerCount;
            int totalConsumption = electricPeopleStation.TotalConsumption + electricRobotStation.TotalConsumption + gasPeopleStation.TotalConsumption + gasRobotStation.TotalConsumption;

            Assert.AreEqual(2, totalCars);
            Assert.AreEqual(1, totalDinners);
            Assert.AreEqual(70, totalConsumption);
        }

        [TestMethod]
        public void TestMissingCarFileStopsProcessing()
        {
            string carJson1 = "{\"id\": 1, \"type\": \"GAS\", \"passengers\": \"PEOPLE\", \"isDining\": true, \"consumption\": 30}";
            string carJson2 = "{\"id\": 2, \"type\": \"ELECTRIC\", \"passengers\": \"ROBOTS\", \"isDining\": false, \"consumption\": 40}";
            File.WriteAllText(Path.Combine(testDirectory, "car1.json"), carJson1);
            File.WriteAllText(Path.Combine(testDirectory, "car2.json"), carJson2);

            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var stations = new List<CarStation> { electricPeopleStation };
            var semaphore = new Semaphore(stations);

            string queueFolder = testDirectory;
            var files = Directory.GetFiles(queueFolder, "car*.json").OrderBy(f => Path.GetFileName(f)).ToList();

            int expectedCarId = 1;
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));
                if (carId != expectedCarId)
                {
                    Assert.Fail($"Missing car file for car {expectedCarId}. Stopping.");
                    break;
                }

                string fileContent = File.ReadAllText(file);
                var car = JsonSerializer.Deserialize<Car>(fileContent);

                if (car != null)
                {
                    semaphore.RouteCar(car);
                }

                expectedCarId++;
            }

            Assert.AreEqual(3, expectedCarId); 
        }
    }
}
