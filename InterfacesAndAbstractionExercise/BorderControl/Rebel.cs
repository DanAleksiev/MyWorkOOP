namespace BorderControl
    {
    public class Rebel : IIndentifyable,IBuyer
        {
        public Rebel(string name, int age,string gang)
            {
            Name = name;
            Age = age;
            Gang = gang;
            Food = 0;
            }

        public string Name { get; }
        public int Age { get; }
        public string Gang { get; }
        public int Food { get; private set; }

        public void BuyFood()
            {
            Food += 5;
            }
        }
    }
