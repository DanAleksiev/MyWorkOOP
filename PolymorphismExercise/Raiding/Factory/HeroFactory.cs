using Raiding.Factory.Interface;
using Raiding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Factory
    {
    public class HeroFactory : IFactory
        {
        public BaseHero AddHeroToTheGroup(string heroName, string heroClass)
            {
            switch (heroClass)
                {
                case "Druid":
                return new Druid(heroName);
                case "Paladin":
                return new Paladin(heroName);
                case "Rogue":
                return new Rogue(heroName);
                case "Warrior":
                return new Warrior(heroName);
                default:
                throw new ArgumentException("Invalid hero!");
                }
            }
        }
    }
