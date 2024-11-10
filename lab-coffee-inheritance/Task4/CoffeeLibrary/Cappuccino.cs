namespace CoffeeLibrary
{
    class Cappuccino : Coffee
    {
        private int mlOfMilk;

        public int MlOfMilk
        {
            get { return mlOfMilk; }
        }

        public Cappuccino(Intensity intensity, string name, int mlOfMilk) : base(intensity, name)
        {
            this.mlOfMilk = mlOfMilk;
        }

        public Cappuccino(int mlOfMilk, Intensity intensity) : base(intensity, "Cappuccino")
        {
            this.mlOfMilk = mlOfMilk;
        }

        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine("Ml of milk: " + mlOfMilk);
        }

        public static Cappuccino MakeCappuccino(int mlOfMilk, Intensity intensity)
        {
            Cappuccino coffee = new Cappuccino(mlOfMilk, intensity);
            coffee.MakeCoffeeBase();
            Console.WriteLine("Amount of milk: " + mlOfMilk + " ml\n");
            return coffee;
        }
    }
}