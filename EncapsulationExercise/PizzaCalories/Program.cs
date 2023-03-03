using System.Security.Cryptography.X509Certificates;

namespace PizzaCalories
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            try
                {
                string[] pizzaName = Console.ReadLine().Split();
                Pizza pizza = new Pizza(pizzaName[1]);

                string[] doughSplit = Console.ReadLine().Split();
                pizza.Dough = new Dough(doughSplit[1], doughSplit[2], int.Parse(doughSplit[3]));

                string input = string.Empty;
                while ((input = Console.ReadLine()) != "END")
                    {
                    string[] inputSplit = input.Split();

                    Topping thisToping = new(inputSplit[1], int.Parse(inputSplit[2]));
                    pizza.AddTopping(thisToping);
                    }
                Console.WriteLine(pizza);
                }
            catch (Exception x)
                {

                Console.WriteLine(x.Message);
                }

            }
        }
    }