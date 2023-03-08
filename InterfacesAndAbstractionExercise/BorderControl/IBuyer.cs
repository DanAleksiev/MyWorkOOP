namespace BorderControl
    {
    public interface IBuyer:IIndentifyable
        {
        public int Food { get; }
        public void BuyFood();
        }
    }
