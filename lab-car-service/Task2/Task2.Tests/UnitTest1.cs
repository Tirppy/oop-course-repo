namespace CarServiceLab
{
    [TestClass]
    public class CarServiceTests
    {
        private StringWriter _consoleOutput;

        [TestInitialize]
        public void TestInitialize()
        {
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TestMethod]
        public void TestServeDinnerForPeopleAndRobotCars()
        {
            var peopleDinner = new PeopleDinner();
            var robotDinner = new RobotDinner();

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30);
            var car2 = new Car(2, "GAS", "PEOPLE", false, 40);
            var car3 = new Car(3, "ELECTRIC", "ROBOTS", true, 50);
            var car4 = new Car(4, "GAS", "ROBOTS", false, 45);

            if (car1.IsDining) peopleDinner.ServeDinner(car1.Id.ToString());
            if (car2.IsDining) peopleDinner.ServeDinner(car2.Id.ToString()); 
            if (car3.IsDining) robotDinner.ServeDinner(car3.Id.ToString());
            if (car4.IsDining) robotDinner.ServeDinner(car4.Id.ToString()); 

            var output = _consoleOutput.ToString();
            Assert.IsTrue(output.Contains("Serving dinner to people in car 1"));
            Assert.IsFalse(output.Contains("Serving dinner to people in car 2"));
            Assert.IsTrue(output.Contains("Serving dinner to robots in car 3"));
            Assert.IsFalse(output.Contains("Serving dinner to robots in car 4"));
        }

        [TestMethod]
        public void TestRefuelElectricAndGasCars()
        {
            var electricStation = new ElectricStation();
            var gasStation = new GasStation();

            var car1 = new Car(1, "ELECTRIC", "PEOPLE", true, 30);
            var car2 = new Car(2, "GAS", "PEOPLE", false, 40);
            var car3 = new Car(3, "ELECTRIC", "ROBOTS", true, 50);
            var car4 = new Car(4, "GAS", "ROBOTS", false, 45);

            if (car1.Type == "ELECTRIC") electricStation.Refuel(car1.Id.ToString());
            if (car2.Type == "GAS") gasStation.Refuel(car2.Id.ToString());
            if (car3.Type == "ELECTRIC") electricStation.Refuel(car3.Id.ToString());
            if (car4.Type == "GAS") gasStation.Refuel(car4.Id.ToString());

            var output = _consoleOutput.ToString();
            Assert.IsTrue(output.Contains("Refueling electric car 1"));
            Assert.IsFalse(output.Contains("Refueling gas car 1"));
            Assert.IsTrue(output.Contains("Refueling electric car 3"));
            Assert.IsFalse(output.Contains("Refueling gas car 3"));
            Assert.IsFalse(output.Contains("Refueling electric car 4"));
        }
    }
}
