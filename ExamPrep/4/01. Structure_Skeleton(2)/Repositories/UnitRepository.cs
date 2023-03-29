using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
    {
    public class UnitRepository : IRepository<IMilitaryUnit>
        {
        private readonly List<IMilitaryUnit> models;

        public UnitRepository()
            {
            this.models = new List<IMilitaryUnit>();
            }

        public IReadOnlyCollection<IMilitaryUnit> Models => this.models;

        public void AddItem(IMilitaryUnit model)
            {
            this.models.Add(model);
            }

        public IMilitaryUnit FindByName(string name)
            {
            IMilitaryUnit unit = models.FirstOrDefault(x=>x.GetType().Name == name);
            return unit;
            }

        public bool RemoveItem(string name)
            {
            IMilitaryUnit unit = models.FirstOrDefault(x => x.GetType().Name == name);
            return models.Remove(unit);
            }
        }
    }
