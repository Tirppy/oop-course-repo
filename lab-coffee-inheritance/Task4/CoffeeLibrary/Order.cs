namespace CoffeeLibrary
{
    
    internal abstract class Order
    {
        public Coffee.Intensity Intensity { get; set; }

        protected Order(Coffee.Intensity intensity)
        {
            Intensity = intensity;
        }
    }

    internal class AmericanoOrder : Order
    {
        public int MlOfWater { get; set; }

        public AmericanoOrder(Coffee.Intensity intensity, int mlOfWater)
            : base(intensity)
        {
            MlOfWater = mlOfWater;
        }
    }

    internal class CappuccinoOrder : Order
    {
        public int MlOfMilk { get; set; }

        public CappuccinoOrder(Coffee.Intensity intensity, int mlOfMilk)
            : base(intensity)
        {
            MlOfMilk = mlOfMilk;
        }
    }

    internal class PumpkinSpiceLatteOrder : Order
    {
        public int MlOfMilk { get; set; }
        public int MgOfPumpkinSpice { get; set; }

        public PumpkinSpiceLatteOrder(Coffee.Intensity intensity, int mlOfMilk, int mgOfPumpkinSpice)
            : base(intensity)
        {
            MlOfMilk = mlOfMilk;
            MgOfPumpkinSpice = mgOfPumpkinSpice;
        }
    }

    internal class SyrupCappuccinoOrder : Order
    {
        public int MlOfMilk { get; set; }
        public SyrupCappuccino.SyrupType SyrupType { get; set; }

        public SyrupCappuccinoOrder(Coffee.Intensity intensity, int mlOfMilk, SyrupCappuccino.SyrupType syrupType)
            : base(intensity)
        {
            MlOfMilk = mlOfMilk;
            SyrupType = syrupType;
        }
    }
}
