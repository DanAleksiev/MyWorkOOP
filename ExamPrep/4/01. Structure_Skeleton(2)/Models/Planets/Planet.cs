using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Weapons
    {
    public class Planet : IPlanet
        {
        private UnitRepository units;
        private WeaponRepository weapons;
        private string name;
        private double budget;
        private double militaryPower;

        public Planet(string name, double budget)
            {
            this.Name = name;
            this.Budget = budget;

            units = new UnitRepository();
            weapons = new WeaponRepository();
            }

        public string Name
            {
            get => this.name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                    }
                this.name = value;
                }
            }
        public double Budget
            {
            get => this.budget;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                    }
                this.budget = value;
                }
            }
        public double MilitaryPower => Math.Round(CalculateTheMilitaryPower(), 3);
        private double CalculateTheMilitaryPower()
            {
            double total = units.Models.Sum(x => x.EnduranceLevel) + weapons.Models.Sum(x => x.DestructionLevel);
            if (units.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
                {
                total *= 1.3;
                }
            if (weapons.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
                {
                total *= 1.45;
                }
            return Math.Round(total, 3);
            }
        public IReadOnlyCollection<IMilitaryUnit> Army => this.units.Models;
        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;
        public void AddUnit(IMilitaryUnit unit)
            {
            units.AddItem(unit);
            }
        public void AddWeapon(IWeapon weapon)
            {
            weapons.AddItem(weapon);
            }
        public void TrainArmy()
            {
            foreach (var unit in this.Army)
                {
                unit.IncreaseEndurance();
                }
            }
        public void Spend(double amount)
            {
            if (this.budget < amount)
                {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
                }
            this.Budget -= amount;
            }
        public void Profit(double amount)
            {
            this.budget += amount;
            }
        public string PlanetInfo()
            {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {this.name}");
            sb.AppendLine($"--Budget: {this.budget} billion QUID");
            sb.Append($"--Forces: ");
            if (this.Army.Count > 0)
                {
                Queue<string> units = new Queue<string>();
                foreach (var unit in this.Army)
                    {
                    units.Enqueue(unit.GetType().Name);
                    }
                sb.Append(string.Join(", ", units));
                }
            else
                {
                sb.Append("No units");
                }
            sb.AppendLine();
            sb.Append($"--Combat equipment: ");
            if (this.Weapons.Count > 0)
                {
                Queue<string> weapons = new Queue<string>();
                foreach (var weapon in this.Weapons)
                    {
                    weapons.Enqueue(weapon.GetType().Name);
                    }

                sb.Append(string.Join(", ", weapons));
                }
            else
                {
                sb.Append("No weapons");
                }
            sb.AppendLine();
            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().Trim();
            }

        }
    }
