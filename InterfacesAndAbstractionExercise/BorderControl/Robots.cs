namespace BorderControl
    {
    public class Robots : IIndentifyable
        {
        public Robots(string name, string id)
            {
            Name = name;
            Id = id;
            }

        public string Name { get; }

        public string Id { get; }

        }
    }
