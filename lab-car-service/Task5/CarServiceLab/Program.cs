using System.Text.Json;

namespace CarServiceLab
{
    class Program
    {
        private static int expectedCarId = 1; 
        private static Semaphore semaphore; 
        private static List<CarStation> stations; 
        private static string queueFolder; 

        static void Main(string[] args)
        {
            
            var electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA(), "ELECTRIC", "PEOPLE");
            var electricRobotStation = new CarStation(new RobotDinner(), new ElectricStation(), new QueueB(), "ELECTRIC", "ROBOTS");
            var gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueC(), "GAS", "PEOPLE");
            var gasRobotStation = new CarStation(new RobotDinner(), new GasStation(), new QueueA(), "GAS", "ROBOTS");

            stations = new List<CarStation> { electricPeopleStation, electricRobotStation, gasPeopleStation, gasRobotStation };

            semaphore = new Semaphore(stations);

            queueFolder = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..", "queue");
            if (!Directory.Exists(queueFolder))
            {
                Console.WriteLine($"Folder '{queueFolder}' does not exist.");
                return;
            }

            System.Timers.Timer readCarTimer = new System.Timers.Timer(4000); 
            readCarTimer.Elapsed += (sender, e) => ProcessNewCars();
            readCarTimer.Start();

            System.Timers.Timer serveCarsTimer = new System.Timers.Timer(10000); 
            serveCarsTimer.Elapsed += (sender, e) => ServeCarsAtStations();
            serveCarsTimer.Start();

            Console.ReadLine();

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

        static void ProcessNewCars()
        {
            try
            {
                var files = Directory.GetFiles(queueFolder, "car*.json")
                                     .OrderBy(f => Path.GetFileName(f))
                                     .ToList();

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
                            Console.WriteLine($"New car added: ID={car.Id}, Type={car.Type}, PassengerType={car.PassengerType}, " +
                                              $"IsDining={car.IsDining}, Consumption={car.Consumption}");
                            expectedCarId++; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing cars: {ex.Message}");
            }
        }

        static void ServeCarsAtStations()
        {
            foreach (var station in stations)
            {
                station.ServeCars();
            }
        }
    }
}
