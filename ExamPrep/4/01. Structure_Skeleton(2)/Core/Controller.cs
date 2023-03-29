using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PlanetWars.Core
    {
    public class Controller : IController
        {
        private readonly PlanetRepository planets;

        public Controller()
            {
            this.planets = new PlanetRepository();
            }
        public string CreatePlanet(string name, double budget)
            {
            if (planets.Models.FirstOrDefault(x => x.Name == name) != null)
                {
                return string.Format(OutputMessages.ExistingPlanet, name);
                }
            planets.AddItem(new Planet(name, budget));
            return string.Format(OutputMessages.NewPlanet, name);
            }

        public string AddUnit(string unitTypeName, string planetName)
            {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
                }
            if (unitTypeName != nameof(SpaceForces) &&
               unitTypeName != nameof(AnonymousImpactUnit) &&
               unitTypeName != nameof(StormTroopers))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
                }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
                }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(SpaceForces))
                {
                unit = new SpaceForces();
                }
            else if (unitTypeName == nameof(AnonymousImpactUnit))
                {
                unit = new AnonymousImpactUnit();
                }
            else
                {
                unit = new StormTroopers();
                }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
            }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
            {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
                }
            else if (weaponTypeName != nameof(BioChemicalWeapon) &&
                weaponTypeName != nameof(NuclearWeapon) &&
                weaponTypeName != nameof(SpaceMissiles))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
                }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
                }
            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
                {
                weapon = new BioChemicalWeapon(destructionLevel);
                }
            else if (weaponTypeName == nameof(NuclearWeapon))
                {
                weapon = new NuclearWeapon(destructionLevel);
                }
            else
                {
                weapon = new SpaceMissiles(destructionLevel);
                }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
            }
        public string SpecializeForces(string planetName)
            {
            IPlanet planet = planets.Models.FirstOrDefault(x => x.Name == planetName);
            if (planet == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
                }
            if (planet.Army.Count == 0)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
                }

            planet.Spend(1.25);
            planet.TrainArmy();
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
            }
        public string SpaceCombat(string planetOne, string planetTwo)
            {
            IPlanet planet1 = planets.Models.FirstOrDefault(x => x.Name == planetOne);
            IPlanet planet2 = planets.Models.FirstOrDefault(x => x.Name == planetTwo);

            IPlanet winner;
            IPlanet looser;
            if (planet1.MilitaryPower == planet2.MilitaryPower)
                {
                bool planet1IsNuclear = planet1.Weapons.Any(x => x.GetType().Name == "NuclearWeapon");
                bool planet2IsNuclear = planet2.Weapons.Any(x => x.GetType().Name == "NuclearWeapon");

                if (planet1IsNuclear && !planet2IsNuclear)
                    {
                    winner = planet1;
                    looser = planet2;
                    }
                else if (!planet1IsNuclear && planet2IsNuclear)
                    {
                    winner = planet2;
                    looser = planet1;
                    }
                else
                    {
                    planet1.Spend(planet1.Budget / 2);
                    planet2.Spend(planet2.Budget / 2);
                    return string.Format(OutputMessages.NoWinner);
                    }
                }
            else
                {
                if (planet1.MilitaryPower > planet2.MilitaryPower)
                    {
                    winner = planet1;
                    looser = planet2;
                    }
                else
                    {
                    winner = planet2;
                    looser = planet1;
                    }
                }
            winner.Spend(winner.Budget / 2);
            double reparetions = planet2.Budget / 2;
            winner.Profit(reparetions);

            double spoilsOfWar = looser.Army.Sum(a => a.Cost)+ looser.Weapons.Sum(a=>a.Price);
            winner.Profit(spoilsOfWar);
            string looserName = looser.Name;
            planets.RemoveItem(looser.Name);
            return string.Format(OutputMessages.WinnigTheWar, winner.Name, looserName);
            }
        public string ForcesReport()
            {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
                {
                stringBuilder.Append(planet.PlanetInfo());
                stringBuilder.AppendLine();
                }

            return stringBuilder.ToString().Trim();
            }
        }
    }