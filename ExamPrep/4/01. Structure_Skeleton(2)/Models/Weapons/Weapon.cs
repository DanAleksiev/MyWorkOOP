using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
    {
    public abstract class Weapon : IWeapon
        {
        private int destructionLevel;
        private double price;

        protected Weapon(int destructionLevel, double price)
            {
            this.DestructionLevel = destructionLevel;
            this.Price = price;
            }

        public double Price
            {
            get => this.price;
            private set
                {
                this.price = value;
                }
            }

        public int DestructionLevel
            {
            get => this.destructionLevel;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);
                    }
                else if (value > 10)
                    {
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);
                    }
                destructionLevel= value;
                }
            }

        }
    }
