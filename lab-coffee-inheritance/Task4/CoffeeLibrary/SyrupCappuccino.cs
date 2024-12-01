namespace CoffeeLibrary
{
    
    internal class SyrupCappuccino : Cappuccino
    {
        public enum SyrupType
        {
            MACADAMIA,
            VANILLA,
            COCONUT,
            CARAMEL,
            CHOCOLATE,
            POPCORN
        }
        private SyrupType syrup;

        
        public SyrupCappuccino(Intensity intensityCoffe, int mltrOfMilk, SyrupType syrup, string coffee = "SyrupCappuccino")
            : base(intensityCoffe, mltrOfMilk, coffee)
        {
            this.syrup = syrup;
        }

        
        public override void PrintCoffeeDetails()
        {
            base.PrintCoffeeDetails();
            Console.WriteLine($"Syrup type: {syrup}");
        }

        
        public SyrupCappuccino MakeSyrupCappuccino()
        {
            base.MakeCappuccino();
            Console.WriteLine($"Adding {syrup} syrup");
            return this;
        }
    }
}