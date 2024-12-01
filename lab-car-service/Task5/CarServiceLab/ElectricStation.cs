namespace CarServiceLab
{
    public class ElectricStation : IRefuelable
    {
        public void Refuel(string carId)
        {
            Console.WriteLine($"Refueling electric car {carId}.");
        }
    }
}
