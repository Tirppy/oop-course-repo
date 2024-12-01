namespace CarServiceLab
{
    public class CarStation
    {
        public IDineable DiningService { get; private set; }
        public IRefuelable RefuelingService { get; private set; }
        public ICarQueue Queue { get; private set; }
        public string CarType { get; private set; }
        public string PassengerType { get; private set; }

        public int CarCount { get; private set; }
        public int DinnerCount { get; private set; }
        public int TotalConsumption { get; private set; }

        public CarStation(IDineable diningService, IRefuelable refuelingService, ICarQueue queue, string carType, string passengerType)
        {
            DiningService = diningService ?? throw new ArgumentNullException(nameof(diningService), "Dining service cannot be null.");
            RefuelingService = refuelingService ?? throw new ArgumentNullException(nameof(refuelingService), "Refueling service cannot be null.");
            Queue = queue ?? throw new ArgumentNullException(nameof(queue), "Queue cannot be null.");
            CarType = carType ?? throw new ArgumentNullException(nameof(carType), "Car type cannot be null.");
            PassengerType = passengerType ?? throw new ArgumentNullException(nameof(passengerType), "Passenger type cannot be null.");
        }

        public void AddCar(Car car)
        {
            Queue.Enqueue(car);
        }

        public void ServeCars()
        {
            while (Queue.Count>0)
            {
                var car = Queue.Dequeue();
                CarCount++;

                if (car.IsDining)
                {
                    DiningService.ServeDinner(car.Id.ToString());
                    DinnerCount++;
                }
                RefuelingService.Refuel(car.Id.ToString());
                TotalConsumption += car.Consumption;
            }
        }

        public bool CanHandleCar(Car car)
        {
            return car.Type == CarType && car.PassengerType == PassengerType;
        }

        public void PrintStats()
        {
            Console.WriteLine($"Station Stats:");
            Console.WriteLine($"Cars processed: {CarCount}");
            Console.WriteLine($"Dinners served: {DinnerCount}");
            Console.WriteLine($"Total consumption: {TotalConsumption}");
        }
    }
}
