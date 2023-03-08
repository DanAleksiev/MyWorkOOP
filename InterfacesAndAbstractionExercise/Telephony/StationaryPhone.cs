namespace Telephony
    {
    public class StationaryPhone : ICallable
        {
        public StationaryPhone(string number)
            {
            Number = number;
            }
        public string Number { get; }

        public virtual void Dial()
            {
            if (ValidateNumber())
                {
                Console.WriteLine($"Dialing... {Number}");
                }
            }

        public  bool ValidateNumber()
            {
            if (Number.Any(x => char.IsLetter(x)))
                {
                Console.WriteLine("Invalid number!");
                return false;
                }
            return true;
            }
        }
    }
