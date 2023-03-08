namespace SquareRoot
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            int number = int.Parse(Console.ReadLine());
            try
                {
                double square = Math.Sqrt(number);
                if (square.)
                    {
                    throw new ArgumentException("Invalid number.");
                    }
                Console.WriteLine(square);
                }
            catch (ArgumentException ex)
                {

                Console.WriteLine(ex.Message);
                }
            finally { Console.WriteLine("Goodbye."); }
            
            }
        }
    }