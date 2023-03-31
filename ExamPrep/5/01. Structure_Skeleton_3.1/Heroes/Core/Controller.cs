using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Heroes.Core
    {
    public class Controller : IController
        {
        private readonly HeroRepository heros;
        private readonly WeaponRepository weapons;

        public Controller()
            {
            this.heros = new HeroRepository();
            this.weapons = new WeaponRepository();
            }

        public string CreateHero(string type, string name, int health, int armour)
            {
            IHero hero = heros.FindByName(name);
            if (hero != null)
                {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
                }

            if (type == nameof(Barbarian))
                {
                hero = new Barbarian(name, health, armour);
                heros.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
                }
            else if (type == nameof(Knight))
                {
                hero = new Knight(name, health, armour);
                heros.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
                }
            else
                {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
                }
            }

        public string CreateWeapon(string type, string name, int durability)
            {
            IWeapon weapon = weapons.FindByName(name);
            if (weapon != null)
                {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
                }

            if (type == nameof(Mace))
                {
                weapon = new Mace(name, durability);
                }
            else if (type == nameof(Claymore))
                {
                weapon = new Claymore(name, durability);
                }
            else
                {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
                }
            weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
            }

        public string AddWeaponToHero(string weaponName, string heroName)
            {
            IWeapon weapon = weapons.FindByName(weaponName);
            IHero hero = heros.FindByName(heroName);

            if (hero == null)
                {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
                }

            if (weapon == null)
                {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
                }

            if (hero.Weapon != null)
                {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
                }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
            }
        public string StartBattle()
            {
            IMap map = new Map();
            List<IHero> group = heros.Models.ToList();
            return map.Fight(group);
            }

        public string HeroReport()
            {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in heros.Models.OrderBy(x=>x.GetType().Name).ThenByDescending(x=>x.Health).ThenBy(x=>x.Name))
                {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon != null)
                    {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                    }
                else
                    {
                    sb.AppendLine("--Weapon: Unarmed");
                    }
                }

            return sb.ToString().Trim();
            }

        }
    }
