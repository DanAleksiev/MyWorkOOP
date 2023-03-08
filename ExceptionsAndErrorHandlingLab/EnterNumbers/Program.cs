namespace EnterNumbers
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            List<int> numbers = new();
            while (numbers.Count < 10)
                {
                try
                    {
                    ReadNumbers(numbers);
                    }
                catch (Exception ex)
                    {
                    Console.WriteLine(ex.Message);
                    }
                }
            Console.WriteLine(string.Join(", ", numbers));
            }

        private static bool ReadNumbers(List<int> numbers)
            {
            Predicate<int> predicate;
            if (numbers.Count == 0)
                {
                predicate = x => x > 1 && x < 100;
                }
            else
                {
                predicate = x => x > numbers[^1] && x < 100;
                }
            if (int.TryParse(Console.ReadLine(), out int result))
                {
                var restrictin = (numbers.Count > 0)
                    ? numbers[^1]
                    : 1;

                if (!predicate(result))
                    {
                    throw new ArgumentException($"Your number is not in range {restrictin} - 100!");
                    }
                numbers.Add(result);
                return true;
                }
            throw new ArgumentException("Invalid Number!");
            }
        }
    }