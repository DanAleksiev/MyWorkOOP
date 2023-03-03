namespace PizzaCalories
    {
    public class Topping
        {
        private const double BaseCalorie = 2;

        private int weight;
        private string topingType;
        private double topingModifier;
        //private double calories;
        public Topping(string topingType, int weight)
            {
            TopingType = topingType;
            Weight = weight;
            }
        public string TopingType
            {
            get => topingType;
            private set
                {
                switch (value.ToLower())
                    {
                    case "meat":
                    topingType = value;
                    topingModifier = 1.2;
                    break;
                    case "veggies":
                    topingType = value;
                    topingModifier = 0.8;
                    break;
                    case "cheese":
                    topingType = value;
                    topingModifier = 1.1;
                    break;
                    case "sauce":
                    topingType = value;
                    topingModifier = 0.9;
                    break;

                    default:
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                    }
                }
            }
        public int Weight
            {
            get => weight;

            private set
                {
                if (value < 1 || value > 50)
                    {
                    throw new ArgumentException($"{this.TopingType} weight should be in the range [1..50].");
                    }
                weight = value;
                }
            }
        public double Calories { get; private set; }

        public double CaloriesCalculaterOfTopings()
            {
            Calories = (BaseCalorie * Weight) * topingModifier;
            return Calories;
            }
        }
    }

