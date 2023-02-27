namespace CustomStack
    {
    public class StartUp
        {
        static void Main(string[] args)
            {
            StackOfStrings stack = new StackOfStrings();
            stack.Push("Hello");
            stack.Push("world");

            Console.WriteLine(stack.IsEmpty());
            }
        }
    }