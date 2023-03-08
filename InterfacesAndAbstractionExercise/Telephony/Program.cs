namespace Telephony
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            string[] phoneNumbers = Console.ReadLine().Split();
            string[] websites = Console.ReadLine().Split();
            foreach (string number in phoneNumbers)
                {
                if (number.Length == 10)
                    {
                    Smartphone smartphone= new Smartphone(number,"132");
                    smartphone.Dial();
                    }
                else if (number.Length == 7)
                    {
                    StationaryPhone homePhone = new StationaryPhone(number);
                    homePhone.Dial();
                    }
                }

            foreach (var site in websites) 
                {
                Smartphone smartphone = new Smartphone("123",site);
                smartphone.Brows();
                }
            }
        }
    }