namespace Shapes
    {
    public class Rectangle : Shape
        {
        private double width;
        private double height;
        public Rectangle(double width, double height)
            {
            Width = width;
            Height = height;
            }

        public double Width
            {
            get { return width; }
            private set { width = value; }
            }
        public double Height
            {
            get { return height; }
            private set { height = value; }
            }
        public override double CalculateArea()
            => this.Width * this.Height;
        public override double CalculatePerimeter()
            => 2 * (this.Width + this.Height);

        }
    }
