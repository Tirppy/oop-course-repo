namespace CarServiceLab
{
    public class PeopleDinner : IDineable
    {
        public void ServeDinner(string carId)
        {
            Console.WriteLine($"Serving dinner to people in car {carId}.");
        }
    }
}
