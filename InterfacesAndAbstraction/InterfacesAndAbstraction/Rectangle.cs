
namespace Shapes
    {
    public class Rectangle : IDrawable
        {
        private int width;
        private int height;

        public Rectangle(int widgth, int height)
            {
            this.width = widgth;
            this.height = height;
            }

        public void Draw()
            {
            DrawLine(this.width, "*","*");
            for (int i = 1; i < this.height-1; ++i)
                {
                DrawLine(this.width, "*", " ");
                }
            DrawLine(this.width, "*", "*");

            }

        private void DrawLine(int widgth, string end, string mid)
            {
            Console.Write(end);
            for (int i = 1; i < this.width - 1; ++i)
                {
                Console.Write(mid);
                }
            Console.WriteLine(end);
            }
        }
    }
