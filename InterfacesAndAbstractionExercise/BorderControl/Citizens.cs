namespace BorderControl
    {
    public class Citizens : Robots, IBirthdayable,IBuyer
        {
        public Citizens(string name, int age, string id, string birthday) : base(name, id)
            {
            Age = age;
            Birthday = birthday;
            Food= 0;
            }

        public int Age { get; }
        public string Birthday { get; }
        public int Food { get; private set; }

        public void BuyFood()
            {
            this.Food += 10;
            }
        }
    }
