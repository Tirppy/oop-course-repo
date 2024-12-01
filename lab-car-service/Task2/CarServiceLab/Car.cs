namespace CarServiceLab
{
    public class Car
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string PassengerType { get; set; }
        public bool IsDining { get; set; }
        public int Consumption { get; set; }

        public Car(int id, string type, string passengerType, bool isDining, int consumption)
        {
            Id = id;
            Type = type;
            PassengerType = passengerType;
            IsDining = isDining;
            Consumption = consumption;
        }
    }
}
