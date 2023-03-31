using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public class Weapon : IWeapon
        {
        private string name;
        private int durability;
        private int damage;

        public Weapon(string name, int durability)
            {
            Name = name;
            Durability = durability;
            }

        public string Name
            {
            get => this.name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                    }
                this.name = value;
                }
            }

        public int Durability
            {
            get => this.durability;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                    }
                this.durability = value;
                }
            }

        public virtual int DoDamage()
            {
            Durability--;
            if (Durability == 0)
                {
                return 0;
                }
            return damage;
            }
        }
    }
