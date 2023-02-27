namespace PersonsInfo
    {
    public class StartUp
        {
        static void Main(string[] args)
            {
            var lines = int.Parse(Console.ReadLine());
            var persons = new List<Person>();
            for (int i = 0; i < lines; i++)
                {
                var input = Console.ReadLine().Split();

                string name = input[0];
                string surname = input[1];
                int age = int.Parse(input[2]);
                decimal salarie = decimal.Parse(input[3]);
                try
                    {
                    var person = new Person(name, surname, age, salarie);
                    persons.Add(person);
                    }
                catch (Exception ex )
                    {
                    Console.WriteLine(ex.Message);
                    }
                }
            var parcentage = decimal.Parse(Console.ReadLine());
            persons.ForEach(p => p.IncreaseSalary(parcentage));
            persons.ForEach(p => Console.WriteLine(p.ToString()));
            }
        }
    }