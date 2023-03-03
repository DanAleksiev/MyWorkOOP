namespace ShoppingSpree
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            string[] peopleEntry = Console.ReadLine().Split(";");
            List<Person> people = new List<Person>();
            List<Product> products = new();

            try
                {
                foreach (var p in peopleEntry)
                    {
                    string[] splitPersonFromMoney = p.Split("=",StringSplitOptions.RemoveEmptyEntries);
                    people.Add(new Person(splitPersonFromMoney[0], decimal.Parse(splitPersonFromMoney[1])));
                    }

                string[] productEntry = Console.ReadLine().Split(";",StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in productEntry)
                    {
                    string[] splitPersonFromMoney = p.Split("=");
                    products.Add(new Product(splitPersonFromMoney[0], decimal.Parse(splitPersonFromMoney[1])));
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return;
                }

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
                {
                string[] splitInput = input.Split(" ");
                string personName = splitInput[0];
                string productName = splitInput[1];

                Person person = people.FirstOrDefault(p => p.Name == personName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (person is not null && product is not null)
                    {
                    Console.WriteLine(person.BuyProduct(product));
                    }
                }
            Console.WriteLine(string.Join(Environment.NewLine, people));
            }
        }
    }