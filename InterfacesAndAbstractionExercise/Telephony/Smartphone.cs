namespace Telephony
    {
    public class Smartphone : StationaryPhone
        {
        public Smartphone(string number,string site) : base(number)
            {
            Site = site;
            }
        public string Site { get; }

        public override void Dial()
            {
            if (ValidateNumber())
                {
                Console.WriteLine($"Calling... {Number}");
                }
            }
        public void Brows()
            {
            if (ValidateWebsite())
                {
                Console.WriteLine($"Browsing: {Site}!");
                }
            }
        public bool ValidateWebsite()
            {
            if (Site.Any(x => char.IsDigit(x)))
                {
                Console.WriteLine("Invalid URL!");
                return false;
                }
            return true;
            }
        }
    }
