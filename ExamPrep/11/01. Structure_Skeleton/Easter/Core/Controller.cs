using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
    {
    public class Controller : IController
        {
        private BunnyRepository bunnys;
        private EggRepository eggs;

        public Controller()
            {
            this.bunnys = new BunnyRepository();
            this.eggs = new EggRepository();
            }

        public string AddBunny(string bunnyType, string bunnyName)
            {
            IBunny bunny = null;
            if (bunnyType == nameof(HappyBunny))
                {
                bunny = new HappyBunny(bunnyName);
                }
            else if (bunnyType == nameof(SleepyBunny))
                {
                bunny = new SleepyBunny(bunnyName);
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
                }
            bunnys.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
            }

        public string AddDyeToBunny(string bunnyName, int power)
            {
            IDye dye = new Dye(power);
            IBunny bunny = bunnys.FindByName(bunnyName);
            if (bunny == null)
                {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
                }

            bunny.AddDye(dye);
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
            }

        public string AddEgg(string eggName, int energyRequired)
            {

            IEgg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
            }

        public string ColorEgg(string eggName)
            {
            IEgg egg = eggs.FindByName(eggName);
            List<IBunny> bunny = bunnys.Models.OrderByDescending(x => x.Energy).Where(x => x.Energy >= 50).ToList();
            IWorkshop workshop = new Workshop();
            if (bunny == null)
                {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
                }

            while (bunny.Count != 0 && !egg.IsDone())
                {
                workshop.Color(egg, bunny[0]);
                IBunny currentB = bunny[0];
                if (currentB.Energy == 0)
                    {
                    bunny.RemoveAt(0);
                    bunnys.Remove(currentB);
                    }
                if (currentB.Dyes.Count(x => x.IsFinished()) == currentB.Dyes.Count )
                    {
                    bunny.RemoveAt(0);
                    }

                

                }
            if (egg.IsDone())
                {
                return string.Format(OutputMessages.EggIsDone, eggName);
                }

            return string.Format(OutputMessages.EggIsNotDone, eggName);
            }

        public string Report()
            {
           StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{eggs.Models.Count(x=>x.IsDone())} eggs are done!");
            sb.AppendLine($"Bunnies info:");
            foreach (var bunny in bunnys.Models.Where(x=>x.Energy>0))
                {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count(x=>!x.IsFinished())} not finished");
                }

            return sb.ToString().Trim();
            }
        }
    }