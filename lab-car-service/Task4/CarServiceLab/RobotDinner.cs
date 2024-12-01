namespace CarServiceLab
{
    public class RobotDinner : IDineable
    {
        public void ServeDinner(string carId)
        {
            Console.WriteLine($"Serving dinner to robots in car {carId}.");
        }
    }
}
