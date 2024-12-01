namespace CarServiceLab
{
    class Program
    {
        static void Main()
        {
            CarStation electricPeopleStation = new CarStation(new PeopleDinner(), new ElectricStation(), new QueueA());
            CarStation electricRobotStation = new CarStation(new RobotDinner(), new ElectricStation(), new QueueB());
            CarStation gasPeopleStation = new CarStation(new PeopleDinner(), new GasStation(), new QueueC());
            CarStation gasRobotStation = new CarStation(new RobotDinner(), new GasStation(), new QueueA());

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30);
            var car2 = new Car(2, "ELECTRIC", "PEOPLE", false, 45);
            var car3 = new Car(3, "GAS", "ROBOTS", true, 25);
            var car4 = new Car(4, "GAS", "PEOPLE", true, 50);

            electricPeopleStation.AddCar(car1);
            electricPeopleStation.AddCar(car2);
            electricRobotStation.AddCar(car3);
            gasPeopleStation.AddCar(car4);

            electricPeopleStation.ServeCars();
            electricRobotStation.ServeCars();
            gasPeopleStation.ServeCars();
        }
    }
}
