using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
    {
    public class PlanetRepository : IRepository<IPlanet>
        {
        private readonly List<IPlanet> models;

        public PlanetRepository()
            {
            this.models = new List<IPlanet>();
            }

        public IReadOnlyCollection<IPlanet> Models => this.models;

        public void AddItem(IPlanet model)
            {
            models.Add(model);
            }

        public IPlanet FindByName(string name)
            {
            IPlanet currentPlanet = models.FirstOrDefault(x => x.Name == name);
            return currentPlanet;
            }

        public bool RemoveItem(string name)
            {
            IPlanet currentPlanet = models.FirstOrDefault(x => x.Name == name);
            return models.Remove(currentPlanet);
            }
        }
    }
