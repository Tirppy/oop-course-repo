namespace CoffeeLibrary
{    
    internal class PumpkinSpiceLatte : Cappuccino
    {
        private int mgOfPumpkinSpice;

        
        public PumpkinSpiceLatte(Intensity intensityOfCoffee, int mlOfMilk, int mgOfPumpkinSpice, string name = "PumpkinSpiceLatte")
            : base(intensityOfCoffee, mlOfMilk, name)
        {
            this.mgOfPumpkinSpice = mgOfPumpkinSpice;
        }

        
        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine($"Pumpkin Spice: {mgOfPumpkinSpice} mg");
        }

        
        public PumpkinSpiceLatte MakePumpkinSpiceLatte()
        {
            base.MakeCappuccino();
            Console.WriteLine($"Adding {mgOfPumpkinSpice} mg of pumpkin spice");
            return this;
        }
    }
}
