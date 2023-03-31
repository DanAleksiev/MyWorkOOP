using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
    {
    public abstract class Hero : IHero
        {
        private string name;
        private int health;
        private IWeapon weapon;
        private int armour;

        public Hero(string name, int health, int armour)
            {
            Name = name;
            Health = health;
            Armour = armour;
            }

        public string Name
            {
            get => this.name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroNameNull));

                    }
                this.name = value;
                }
            }

        public int Health
            {
            get => this.health;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroHealthBelowZero));
                    }
                this.health = value;
                }
            }

        public int Armour
            {
            get => this.armour;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                    }
                this.armour = value;
                }
            }
        public IWeapon Weapon
            {
            get => this.weapon;
            private set
                {
                if (value == null)
                    {
                    throw new ArgumentException(ExceptionMessages.WeaponNull);
                    }
                this.weapon = value;
                }
            }

        public bool IsAlive => this.health > 0;

        public void AddWeapon(IWeapon weapon)
            {
            if (weapon != null)
                {
                this.Weapon = weapon;
                }
            }

        public void TakeDamage(int points)
            {
            if (this.Armour > 0 && this.Armour > points)
                {
                Armour -= points;
                }
            else
                {
                points -= Armour;
                Armour = 0;

                if (Health - points < 0)
                    {
                    health = 0;
                    }
                else
                    {
                    Health -= points;
                    }
                }

            }
        }
    }
