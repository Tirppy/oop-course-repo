namespace CarServiceLab
{
    public class GasStation : IRefuelable
    {
        public void Refuel(string carId)
        {
            Console.WriteLine($"Refueling gas car {carId}.");
        }
    }
}