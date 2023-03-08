using Raiding.Models;
using Raiding.Core.interfaces;
using Raiding.IO.Interfaces;
using Raiding.IO;
using Raiding.Factory.Interface;

namespace Raiding.Core
    {
    public class Engine : IEngine
        {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IFactory factory;

        private readonly ICollection<BaseHero> raidGroup;
        public Engine(IReader reader, IWriter writer ,IFactory factory)
            {
            this.reader = reader;
            this.writer = writer;
            this.factory = factory;
            raidGroup = new List<BaseHero>();
            }

        public void Run()
            {
            int membersCount = int.Parse(reader.ReadLine());

            while (membersCount > raidGroup.Count)
                {
                try
                    {
                    ProcessCommand();
                    }
                catch (ArgumentException ex)
                    {
                    Console.WriteLine(ex.Message);
                    }

                }
            int bossPowerLevel = int.Parse(reader.ReadLine());
            int groupPowerLevel = 0;
            foreach (var member in raidGroup)
                {
                groupPowerLevel += member.Power;
                writer.WriteLine(member.CastAbility());
                }

            if (bossPowerLevel > groupPowerLevel)
                {
                writer.WriteLine("Defeat...");
                }
            else
                {
                writer.WriteLine("Victory!");
                }
            }

        private void ProcessCommand()
            {
            string heroName = reader.ReadLine();
            string heroClass = reader.ReadLine();
            raidGroup.Add(factory.AddHeroToTheGroup(heroName, heroClass));
            }
        }
    }

