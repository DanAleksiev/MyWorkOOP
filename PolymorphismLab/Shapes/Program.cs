namespace Shapes
    {
    public class StartUp
        {
        static void Main(string[] args)
            {
            Shape rectangle = new Rectangle(10, 20);
            Shape circle = new Circle(30);

            List<Shape> shapes = new List<Shape>();
            shapes.Add(rectangle);
            shapes.Add(circle);

            foreach (Shape shape in shapes)
                {
                shape.CalculatePerimeter();
                }
            }
        }
    }