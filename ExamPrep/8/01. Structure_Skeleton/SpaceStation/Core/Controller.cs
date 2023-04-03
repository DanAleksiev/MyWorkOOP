using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Missions;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
    {
    public class Controller : IController
        {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private int exploredPlanets = 0;

        public Controller()
            {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
            }

        public string AddAstronaut(string type, string astronautName)
            {
            if (type != nameof(Geodesist) &&
                type != nameof(Meteorologist) &&
                type != nameof(Biologist))
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
                }

            IAstronaut astro;
            if (type == nameof(Meteorologist))
                {
                astro = new Meteorologist(astronautName);
                }
            else if (type == nameof(Biologist))
                {
                astro = new Biologist(astronautName);
                }
            else
                {
                astro = new Geodesist(astronautName);
                }
            astronauts.Add(astro);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
            }

        public string AddPlanet(string planetName, params string[] items)
            {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
                {
                planet.Items.Add(item);
                }
            planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
            }

        public string RetireAstronaut(string astronautName)
            {
            if (astronauts.FindByName(astronautName) == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
                }
            astronauts.Remove(astronauts.FindByName(astronautName));

            return string.Format(OutputMessages.AstronautRetired, astronautName);
            }


        public string ExplorePlanet(string planetName)
            {
            List<IAstronaut> suitable = new List<IAstronaut>();
            foreach (var astro in astronauts.Models.Where(x => x.Oxygen > 60))
                {
                suitable.Add(astro);
                }
            if (suitable.Count == 0)
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
                }
            IPlanet planet = planets.FindByName(planetName);
            IMission mission = new Mission();
            mission.Explore(planet, suitable);
            exploredPlanets++;
            int countDeadAstronauts = suitable.Count(x => x.Oxygen == 0);
            return string.Format(OutputMessages.PlanetExplored, planetName, countDeadAstronauts);
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanets} planets were explored!");
            sb.AppendLine($"Astronauts info:");
            foreach (var astronaut in astronauts.Models)
                {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.Append($"Bag items: ");
                IBag bag = astronaut.Bag;
                if (bag.Items.Count > 0)
                    {
                    
                        sb.Append(string.Join(", ", bag.Items));
                    }
                else
                    {
                    sb.Append($"none");
                    }
                sb.AppendLine($"");
                }

            return sb.ToString().Trim();
            }

        }
    }