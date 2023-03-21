namespace AuthorProblem
    {
    [Author("Strahil")]
    public class StartUp
        {
        [Author("Grisho")]

        static void Main(string[] args)
            {
            var tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
            }
        }
    }