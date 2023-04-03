using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Missions
    {
    public class Mission : IMission
        {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
            {
            List<string> items = planet.Items.ToList();
            foreach (var ast in astronauts.Where(x => x.Oxygen > 0))
                {
                while (ast.Oxygen > 0 && items.Count > 0)
                    {
                    string currentItem = items[0];
                    items.RemoveAt(0);
                    ast.Bag.Items.Add(currentItem);
                    ast.Breath();
                    planet.Items.Remove(currentItem);
                    }
                if (items.Count == 0) break;
                }
            }
        }
    }
