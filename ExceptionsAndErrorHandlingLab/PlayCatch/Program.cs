namespace PlayCatch
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            int[] list = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int exCount = 0;

            while (exCount < 3)
                {
                string[] input = Console.ReadLine().Split();
                string action = input[0];
                try
                    {
                    if (action == "Replace")
                        {
                        int targetIndex = int.Parse(input[1]);
                        int replaceWith = int.Parse(input[2]);
                        list[targetIndex] = replaceWith;
                        }
                    if (action == "Print")
                        {
                        int startIndex = int.Parse(input[1]);
                        int endIndex = int.Parse(input[2])+1;
                        Console.WriteLine(string.Join(", ", list[startIndex..endIndex]));
                        }
                    if (action == "Show")
                        {
                        int index = int.Parse(input[1]);
                        Console.WriteLine(list[index]);
                        }
                    }
                catch (IndexOutOfRangeException )
                    {
                    Console.WriteLine("The index does not exist!");
                    exCount++;
                    }                       
                catch (ArgumentOutOfRangeException)
                    {
                    Console.WriteLine("The index does not exist!");
                    exCount++;
                    }            
                catch (FormatException )
                    {
                    Console.WriteLine("The variable is not in the correct format!");
                    exCount++;
                    }
                }

            Console.WriteLine(string.Join(", ", list));
            }
        }
    }