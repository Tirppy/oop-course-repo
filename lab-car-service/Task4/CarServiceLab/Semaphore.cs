namespace CarServiceLab
{
    public class Semaphore
    {
        private readonly List<CarStation> _stations;

        public Semaphore(List<CarStation> stations)
        {
            _stations = stations ?? throw new ArgumentNullException(nameof(stations), "Stations cannot be null.");
        }

        public void RouteCar(Car car)
        {
            foreach (var station in _stations)
            {
                if (station.CanHandleCar(car))
                {
                    station.AddCar(car);
                    return;
                }
            }

            Console.WriteLine($"No suitable station found for car {car.Id}.");
        }
    }
}
