namespace BorderControl
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            int buyers = int.Parse(Console.ReadLine());
            List<IBuyer> customers = new List<IBuyer>();

            for (int i = 0; i < buyers; i++)
                {
                string[] inputSplit = Console.ReadLine().Split();
                if (inputSplit.Length == 3)
                    {
                    Rebel thug = new Rebel(inputSplit[0], int.Parse(inputSplit[1]), inputSplit[2]);
                    customers.Add(thug);
                    }
                else
                    {
                    Citizens townBoy = new Citizens(inputSplit[0], int.Parse(inputSplit[1]), inputSplit[2], inputSplit[3]);
                    customers.Add(townBoy);
                    }
                }
            string input;
            while((input = Console.ReadLine()) != "End")
                {
                foreach (var c in customers)
                    {
                    if (c.Name == input)
                        {
                        c.BuyFood();
                        }
                    }
                }

            Console.WriteLine(customers.Sum(x => x.Food));
            //string input;
            //List<IBirthdayable> members = new List<IBirthdayable>();
            //while ((input = Console.ReadLine()) != "End")
            //    {
            //    string[] splitInput = input.Split();
            //    string entaty = splitInput[0];
            //    if (entaty == "Robot")
            //        {
            //        //Robots robot = new Robots(splitInput[0], splitInput[1]);
            //        //members.Add(robot);
            //        }
            //    else if (entaty == "Citizen")
            //        {
            //        Citizens citizen = new Citizens(splitInput[1], int.Parse(splitInput[2]), splitInput[3], splitInput[4]);
            //        members.Add(citizen);
            //        }
            //    else if (entaty == "Pet")
            //        {
            //        Pet pet = new Pet(splitInput[1], splitInput[2]);
            //        members.Add(pet);
            //        }
            //    }
            //string target = Console.ReadLine();
            //foreach (var individual in members)
            //    {
            //    if (individual.Birthday.EndsWith(target))
            //        {
            //        Console.WriteLine(individual.Birthday);
            //        }
            //    }
            }
        }
    }