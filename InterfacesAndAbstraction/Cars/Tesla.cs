namespace Cars
    {
    public class Tesla : ICar, IElectricCar
        {
        public Tesla(string model, string color, int battery)
            {
            Model = model;
            Color = color;
            Battery = battery;
            }

        public string Model { get; }
        public string Color { get; }
        public int Battery { get; }

        public string Start()
            {
            return "Engine start";
            }

        public string Stop()
            {
            return "Breaaak!";
            }

        public override string ToString()
            {
            return $"{Color} Tesla {Model} with {Battery} Batteries{Environment.NewLine}{Start()}{Environment.NewLine}{Stop()}";
            }
        }
    }
