namespace CoffeeLibrary
{
    class SyrupCappuccino : Cappuccino
    {
        private SyrupType syrup;

        public SyrupType Syrup
        {
            get { return syrup; }
        }

        public enum SyrupType
        {
            MACADAMIA,
            VANILLA,
            COCONUT,
            CARAMEL,
            CHOCOLATE,
            POPCORN
        }

        public SyrupCappuccino(Intensity intensity, SyrupType syrup, int mlOfMilk) 
            : base(intensity, "Syrup Cappuccino", mlOfMilk)
        {
            this.syrup = syrup;
        }

        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine("Syrup Type: " + syrup + "\n");
        }

        public static SyrupCappuccino MakeSyrupCappuccino(int mlOfMilk, SyrupType syrup, Intensity intensity)
        {
            SyrupCappuccino coffee = new SyrupCappuccino(intensity, syrup, mlOfMilk);
            coffee.MakeCoffeeBase();
            Console.WriteLine("Amount of milk: " + mlOfMilk + " ml");
            Console.WriteLine("Syrup type: " + syrup + "\n");
            return coffee;
        }
    }
}