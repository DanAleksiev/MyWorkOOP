namespace Cards
    {
    public class Program
        {
        static void Main(string[] args)
            {
            string[] InputCards = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            List<Card> deck = new List<Card>();
            foreach (string card in InputCards)
                {
                try
                    {
                    Valid(card, ref deck);              
                    }
                catch (ArgumentException ex)
                    {
                    Console.WriteLine(ex.Message);
                    }
                }
            Console.WriteLine(string.Join(" ",deck));
            }

        public static bool Valid(string card, ref List<Card> deck)
            {
            string[] cardInfo = card.Split();
            string face = cardInfo[0];
            string suit = cardInfo[1];
            if (ValidateFace(face) && ValidateSuit(suit))
                {
                Card validCard = new Card(face, suit);

                validCard.ToString();
                deck.Add(validCard);
                return true;

                }
            throw new ArgumentException("Invalid card!");

            }

        private static bool ValidateSuit(string suit)
            {
            switch (suit)
                {
                case "S":
                case "H":
                case "D":
                case "C":
                return true;

                default:
                return false;
                }
            }

        private static bool ValidateFace(string face)
            {
            switch (face)
                {
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "10":
                case "J":
                case "Q":
                case "K":
                case "A":
                return true;

                default:
                return false;
                }
            }
        }
    public class Card
        {
        private readonly List<Card> cards;
        public Card(string face, string suit)
            {
            Face = face;
            Suit = suit;
            }

        public string Face { get; set; }
        public string Suit { get; set; }

        public override string ToString()
            {
            string symbal = string.Empty;
            if (Suit == "S")
                symbal = "\u2660";
            if (Suit == "H")
                symbal = "\u2665";
            if (Suit == "D")
                symbal = "\u2666";
            if (Suit == "C")
                symbal = "\u2663";

            return $"[{Face}{symbal}]";
            }

        }
    }