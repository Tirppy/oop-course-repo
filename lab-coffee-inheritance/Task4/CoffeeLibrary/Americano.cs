namespace CoffeeLibrary
{
    class Americano : Coffee
    {
        private int mlOfWater;

        public int MlOfWater
        {
            get { return mlOfWater; }
        }

        public Americano(int mlOfWater, Intensity intensity) : base(intensity, "Americano")
        {
            this.mlOfWater = mlOfWater;
        }

        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine("Ml of water: " + mlOfWater + "\n");
        }

        public static Americano MakeAmericano(int mlOfWater, Intensity intensity)
        {
            Americano coffee = new Americano(mlOfWater, intensity);
            coffee.MakeCoffeeBase();
            Console.WriteLine("Amount of water: " + mlOfWater + " ml\n");
            return coffee;
        }
    }
}