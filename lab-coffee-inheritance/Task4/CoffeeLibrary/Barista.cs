namespace CoffeeLibrary
{
    public class Barista
    {
        private List<Order> orders;

        public Barista()
        {
            orders = new List<Order>();
        }
        
        public void StartOrder()
        {
            Console.WriteLine("Welcome to the Coffee Shop!");

            bool ordering = true;
            while (ordering)
            {
                Console.WriteLine("Please select a coffee to order:");
                Console.WriteLine("1. Americano");
                Console.WriteLine("2. Cappuccino");
                Console.WriteLine("3. Pumpkin Spice Latte");
                Console.WriteLine("4. Syrup Cappuccino");
                Console.WriteLine("5. Finish order");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        orders.Add(OrderAmericano());
                        break;
                    case 2:
                        orders.Add(OrderCappuccino());
                        break;
                    case 3:
                        orders.Add(OrderPumpkinSpiceLatte());
                        break;
                    case 4:
                        orders.Add(OrderSyrupCappuccino());
                        break;
                    case 5:
                        ordering = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

            foreach (Order order in orders)
            {
                Coffee coffee = null;
                switch (order.CoffeeType)
                {
                    case "Americano":
                        coffee = Americano.MakeAmericano(order.MlOfWater, order.Intensity);
                        break;
                    case "Cappuccino":
                        coffee = Cappuccino.MakeCappuccino(order.MlOfMilk, order.Intensity);
                        break;
                    case "Pumpkin Spice Latte":
                        coffee = PumpkinSpiceLatte.MakeLatte(order.MlOfMilk, order.MgOfPumpkinSpice, order.Intensity);
                        break;
                    case "Syrup Cappuccino":
                        coffee = SyrupCappuccino.MakeSyrupCappuccino(order.MlOfMilk, order.SyrupType, order.Intensity);
                        break;
                }
            }

            Console.WriteLine("Thank you for your order!");
        }

        private Order OrderAmericano()
        {
            Console.WriteLine("Ordering an Americano.");
            Coffee.Intensity intensity = GetIntensity();
            int mlOfWater = GetMlOfWater();

            return new Order("Americano", intensity) { MlOfWater = mlOfWater };
        }

        private Order OrderCappuccino()
        {
            Console.WriteLine("Ordering a Cappuccino.");
            Coffee.Intensity intensity = GetIntensity();
            int mlOfMilk = GetMlOfMilk();

            return new Order("Cappuccino", intensity) { MlOfMilk = mlOfMilk };
        }
        
        private Order OrderPumpkinSpiceLatte()
        {
            Console.WriteLine("Ordering a Pumpkin Spice Latte.");
            Coffee.Intensity intensity = GetIntensity();
            int mlOfMilk = GetMlOfMilk();
            int mgOfPumpkinSpice = GetMgOfPumpkinSpice();

            return new Order("Pumpkin Spice Latte", intensity) { MlOfMilk = mlOfMilk, MgOfPumpkinSpice = mgOfPumpkinSpice };
        }
        
        private Order OrderSyrupCappuccino()
        {
            Console.WriteLine("Ordering a Syrup Cappuccino.");
            Coffee.Intensity intensity = GetIntensity();
            int mlOfMilk = GetMlOfMilk();
            SyrupCappuccino.SyrupType syrup = GetSyrupType();

            return new Order("Syrup Cappuccino", intensity) { MlOfMilk = mlOfMilk, SyrupType = syrup };
        }

        private Coffee.Intensity GetIntensity()
        {
            Console.WriteLine("Select intensity:");
            Console.WriteLine("1. LIGHT");
            Console.WriteLine("2. NORMAL");
            Console.WriteLine("3. STRONG");
            int choice = int.Parse(Console.ReadLine());
            return choice switch
            {
                1 => Coffee.Intensity.LIGHT,
                2 => Coffee.Intensity.NORMAL,
                3 => Coffee.Intensity.STRONG,
                _ => Coffee.Intensity.NORMAL
            };
        }

        private int GetMlOfWater()
        {
            Console.WriteLine("Select ml of water:");
            Console.WriteLine("1. 50 ml");
            Console.WriteLine("2. 100 ml");
            Console.WriteLine("3. 150 ml");
            int choice = int.Parse(Console.ReadLine());
            return choice switch
            {
                1 => 50,
                2 => 100,
                3 => 150,
                _ => 100
            };
        }

        private int GetMlOfMilk()
        {
            Console.WriteLine("Select ml of milk:");
            Console.WriteLine("1. 50 ml");
            Console.WriteLine("2. 100 ml");
            Console.WriteLine("3. 150 ml");
            int choice = int.Parse(Console.ReadLine());
            return choice switch
            {
                1 => 50,
                2 => 100,
                3 => 150,
                _ => 100
            };
        }

        private int GetMgOfPumpkinSpice()
        {
            Console.WriteLine("Select mg of pumpkin spice:");
            Console.WriteLine("1. 10 mg");
            Console.WriteLine("2. 20 mg");
            Console.WriteLine("3. 30 mg");
            int choice = int.Parse(Console.ReadLine());
            return choice switch
            {
                1 => 10,
                2 => 20,
                3 => 30,
                _ => 20
            };
        }

        private SyrupCappuccino.SyrupType GetSyrupType()
        {
            Console.WriteLine("Select syrup:");
            Console.WriteLine("1. MACADAMIA");
            Console.WriteLine("2. VANILLA");
            Console.WriteLine("3. COCONUT");
            Console.WriteLine("4. CARAMEL");
            Console.WriteLine("5. CHOCOLATE");
            Console.WriteLine("6. POPCORN");
            int choice = int.Parse(Console.ReadLine());
            return choice switch
            {
                1 => SyrupCappuccino.SyrupType.MACADAMIA,
                2 => SyrupCappuccino.SyrupType.VANILLA,
                3 => SyrupCappuccino.SyrupType.COCONUT,
                4 => SyrupCappuccino.SyrupType.CARAMEL,
                5 => SyrupCappuccino.SyrupType.CHOCOLATE,
                6 => SyrupCappuccino.SyrupType.POPCORN,
                _ => SyrupCappuccino.SyrupType.POPCORN
            };
        }

        private class Order
        {
            public string CoffeeType { get; }
            public Coffee.Intensity Intensity { get; }
            public int MlOfMilk { get; set; }
            public int MlOfWater { get; set; }
            public int MgOfPumpkinSpice { get; set; }
            public SyrupCappuccino.SyrupType SyrupType { get; set; }

            public Order(string coffeeType, Coffee.Intensity intensity)
            {
                CoffeeType = coffeeType;
                Intensity = intensity;
                MlOfMilk = 0;
                MlOfWater = 0;
                MgOfPumpkinSpice = 0;
                SyrupType = default;
            }
        }
    }
}
