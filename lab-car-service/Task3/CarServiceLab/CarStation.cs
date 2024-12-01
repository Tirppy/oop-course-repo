namespace CarServiceLab
{
    public class CarStation
    {
        public IDineable DiningService { get; private set; }
        public IRefuelable RefuelingService { get; private set; }
        public ICarQueue Queue { get; private set; }

        public CarStation(IDineable diningService, IRefuelable refuelingService, ICarQueue queue)
        {
            DiningService = diningService ?? throw new ArgumentNullException(nameof(diningService), "Dining service cannot be null.");
            RefuelingService = refuelingService ?? throw new ArgumentNullException(nameof(refuelingService), "Refueling service cannot be null.");
            Queue = queue ?? throw new ArgumentNullException(nameof(queue), "Queue cannot be null.");
        }

        public void AddCar(Car car)
        {
            Queue.Enqueue(car);
        }

        public void ServeCars()
        {
            while (Queue.Count > 0)
            {
                var car = Queue.Dequeue();
                if (car.IsDining)
                {
                    DiningService.ServeDinner(car.Id.ToString());
                }
                RefuelingService.Refuel(car.Id.ToString());
            }
        }
    }
}