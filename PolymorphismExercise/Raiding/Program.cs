
using Raiding.Core;
using Raiding.Core.interfaces;
using Raiding.Factory;
using Raiding.Factory.Interface;
using Raiding.IO;
using Raiding.IO.Interfaces;

namespace Raiding
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IFactory factory = new HeroFactory();

            IEngine engine = new Engine(reader, writer, factory);

            engine.Run();
            }
        }
    }