namespace CoffeeLibrary
{
    class PumpkinSpiceLatte : Cappuccino
    {
        private int mgOfPumpkinSpice;

        public int MgOfPumpkinSpice
        {
            get { return mgOfPumpkinSpice; }
        }

        public PumpkinSpiceLatte(Intensity intensity, int mgOfPumpkinSpice, int mlOfMilk) 
            : base(intensity, "Pumpkin spice latte", mlOfMilk)
        {
            this.mgOfPumpkinSpice = mgOfPumpkinSpice;
        }

        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine("Mg of Pumpkin Spice: " + mgOfPumpkinSpice + "\n");
        }

        public static PumpkinSpiceLatte MakeLatte(int mlOfMilk, int mgOfPumpkinSpice, Intensity intensity)
        {
            PumpkinSpiceLatte coffee = new PumpkinSpiceLatte(intensity, mgOfPumpkinSpice, mlOfMilk);
            coffee.MakeCoffeeBase();
            Console.WriteLine("Amount of milk: " + mlOfMilk + " ml");
            Console.WriteLine("Amount of pumpkin spice: " + mgOfPumpkinSpice + " mg\n");
            return coffee;
        }
    }
}
