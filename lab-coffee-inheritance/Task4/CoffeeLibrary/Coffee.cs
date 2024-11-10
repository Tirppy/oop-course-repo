namespace CoffeeLibrary
{
    class Coffee
    {
        public enum Intensity
        {
            LIGHT,
            NORMAL,
            STRONG
        }

        public Intensity CoffeeIntensity { get; set; }
        private string name;

        public Coffee(Intensity coffeeIntensity, string name)
        {
            this.CoffeeIntensity = coffeeIntensity;
            this.name = name;
        }

        public virtual void PrintCoffeeDetails()
        {
            Console.WriteLine("You wanted: " + name + "\nwith intensity: " + CoffeeIntensity);
        }

        public void MakeCoffeeBase()
        {
            Console.WriteLine("I am making: " + name + "\nIntensity set to: " + CoffeeIntensity);
        }
    }
}