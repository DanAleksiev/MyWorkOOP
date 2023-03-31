using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models
    {
    public class Map : IMap
        {
        private readonly HeroRepository knites;
        private readonly HeroRepository barberians;

        public Map()
            {
            this.knites = new HeroRepository();
            this.barberians = new HeroRepository();
            }

        public string Fight(ICollection<IHero> players)
            {
            foreach (var hero in players.Where(x => x.IsAlive && x.Weapon != null))
                {
                if (hero.GetType().Name == nameof(Knight))
                    {
                    knites.Add(hero);
                    }
                else
                    {
                    barberians.Add(hero);
                    }
                }

            while (true)
                {
                foreach (var hero in knites.Models)
                    {
                    if (hero.IsAlive && hero.Weapon.Durability != 0)
                        {
                        foreach (var barb in barberians.Models)
                            {
                            if (barb.IsAlive)
                                {
                                int damage = hero.Weapon.DoDamage();
                                barb.TakeDamage(damage);
                                }

                            if (hero.Weapon.Durability == 0)
                                {
                                break;
                                }
                            }
                        }
                    }

                foreach (var barb in barberians.Models)
                    {
                    if (barb.IsAlive && barb.Weapon.Durability != 0)
                        {
                        foreach (var hero in knites.Models)
                            {
                            if (hero.IsAlive)
                                {
                                int damage = barb.Weapon.DoDamage();
                                hero.TakeDamage(damage);
                                }
                            if (barb.Weapon.Durability == 0)
                                {
                                break;
                                }
                            }
                        }
                    }


                bool anyKnights = knites.Models.Any(x => x.IsAlive == true);
                bool anyBarberians = barberians.Models.Any(x => x.IsAlive == true);

                if (!anyBarberians)
                    {
                    int casualty = 0;
                    foreach (var k in knites.Models)
                        {
                        if (!k.IsAlive)
                            {
                            casualty++;
                            }
                        }
                    return string.Format(OutputMessages.MapFightKnightsWin, casualty);
                    }
                if (!anyKnights)
                    {
                    int casualty = 0;
                    foreach (var b in barberians.Models)
                        {
                        if (!b.IsAlive)
                            {
                            casualty++;
                            }
                        }
                    int numCasualties = barberians.Models.Count - barberians.Models.Count(x => x.IsAlive);
                    return string.Format(OutputMessages.MapFigthBarbariansWin, casualty);
                    }
                }
            }
        }
    }
