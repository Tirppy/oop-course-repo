namespace CoffeeLibrary
{
    
    internal class Cappuccino : Coffee
    {
        private int mlOfMilk;

        
        public Cappuccino(Intensity intensity, int mlOfMilk, string coffee = "Cappuccino") 
            : base(intensity, coffee)
        {
            this.mlOfMilk = mlOfMilk;
        }

        
        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine($"Milk: {mlOfMilk} ml");
        }

        
        public Cappuccino MakeCappuccino()
        {
            base.MakeCoffee();
            Console.WriteLine($"Adding {mlOfMilk} ml of milk");
            return this;
        }
    }
}