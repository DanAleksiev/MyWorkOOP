namespace PizzaCalories
    {
    public class Pizza
        {
        private string name;
        private readonly List<Topping> toping;
        public Pizza(string name)
            {
            Name = name;
            toping = new List<Topping>();
            }
        public string Name
            {
            get => name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
                    {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                    }
                name = value;
                }
            }
        public Dough Dough { get; set; }

        public int CountOfToppings => toping.Count;

        public void AddTopping(Topping top)
            {
            toping.Add(top);
            if (toping.Count > 10)
                {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
                }
            }
        private double TotalCalories()
            {
            double totalCalories = Dough.CaloriesCalculaterOfDough();
            foreach (var topping in toping)
                {
                totalCalories += topping.CaloriesCalculaterOfTopings();
                }
            return totalCalories;
            }

        public override string ToString()
            {
            double calories = TotalCalories();
            return $"{Name} - {calories:f2} Calories.";
            }
        }
    }
