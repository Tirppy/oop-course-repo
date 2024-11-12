namespace CoffeeLibrary
{
    
    internal class Americano : Coffee
    {
        private int mlOfWater;

        
        public Americano(Intensity intensityCoffee, int mlOfWater, string coffeeName = "Americano")
            : base(intensityCoffee, coffeeName)
        {
            this.mlOfWater = mlOfWater;
        }

        
        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine($"Water: {mlOfWater} ml");
        }

        
        public Americano MakeAmericano()
        {
            base.MakeCoffee();
            Console.WriteLine($"Adding {mlOfWater} ml of water");
            return this;
        }
    }
}