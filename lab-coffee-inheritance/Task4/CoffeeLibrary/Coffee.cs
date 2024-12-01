namespace CoffeeLibrary
{
    
    internal class Coffee
    {
        public enum Intensity
        {
            LIGHT,
            NORMAL,
            STRONG
        }
        private Intensity coffeeIntensity;
        private string name;

        
        public Coffee(Intensity coffeeIntensity, string name = "Coffee")
        {
            this.coffeeIntensity = coffeeIntensity;
            this.name = name;
        }

        
        public virtual void PrintCoffeeDetails()
        {
            Console.WriteLine($"Recipe for {name}");
            Console.WriteLine($"Coffee intensity: {coffeeIntensity}");
        }

        
        public Coffee MakeCoffee()
        {
            Console.WriteLine($"Making {name}");
            Console.WriteLine($"Setting intensity to {coffeeIntensity}");
            return new Coffee(coffeeIntensity, name);
        }
    }
}