using System.Text.Json;

namespace CarServiceLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var electricRobotStation = new CarStation(new RobotDinner(), new ElectricStation(), new QueueB(), "ELECTRIC", "ROBOTS");
            var gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueC(), "GAS", "PEOPLE");
            var gasRobotStation = new CarStation(new RobotDinner(), new GasStation(), new QueueA(), "GAS", "ROBOTS");

            var stations = new List<CarStation> { electricPeopleStation, electricRobotStation, gasPeopleStation, gasRobotStation };

            var semaphore = new Semaphore(stations);

            string queueFolder = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..", "queue");
            if (!Directory.Exists(queueFolder))
            {
                Console.WriteLine($"Folder '{queueFolder}' does not exist.");
                return;
            }

            var files = Directory.GetFiles(queueFolder, "car*.json")
                                 .OrderBy(f => Path.GetFileName(f)) 
                                 .ToList();

            int expectedCarId = 1; 

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                int carId = int.Parse(fileName.Substring(3, fileName.Length - 8));
                if (carId != expectedCarId)
                {
                    Console.WriteLine($"Missing car file for car {expectedCarId}. Stopping.");
                    break; 
                }

                try
                {
                    var json = File.ReadAllText(file);
                    var car = JsonSerializer.Deserialize<Car>(json);

                    if (car != null)
                    {
                        semaphore.RouteCar(car);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file}: {ex.Message}");
                }

                expectedCarId++;
            }

            foreach (var station in stations)
            {
                Console.WriteLine($"Serving cars at station: {station.CarType} - {station.PassengerType}");
                station.ServeCars();
            }

            electricPeopleStation.PrintStats();
            electricRobotStation.PrintStats();
            gasPeopleStation.PrintStats();
            gasRobotStation.PrintStats();

            int totalCars = electricPeopleStation.CarCount + electricRobotStation.CarCount + gasPeopleStation.CarCount + gasRobotStation.CarCount;
            int totalDinners = electricPeopleStation.DinnerCount + electricRobotStation.DinnerCount + gasPeopleStation.DinnerCount + gasRobotStation.DinnerCount;
            int totalConsumption = electricPeopleStation.TotalConsumption + electricRobotStation.TotalConsumption + gasPeopleStation.TotalConsumption + gasRobotStation.TotalConsumption;

            Console.WriteLine($"Total Stats:");
            Console.WriteLine($"Cars processed: {totalCars}");
            Console.WriteLine($"Dinners served: {totalDinners}");
            Console.WriteLine($"Total consumption: {totalConsumption}L");
        }
    }
}
