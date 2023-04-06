using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
    {
    public class Bunny : IBunny
        {
        private string name;
        private int energy;
        private List<IDye> dyes;

        public Bunny(string name, int energy)
            {
            Name = name;
            Energy = energy;

            dyes = new List<IDye>();
            }

        public string Name
            {
            get => name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                    }
                name = value;
                }
            }

        public int Energy
            {
            get => energy;
            private set => energy = value;
            }

        public ICollection<IDye> Dyes => dyes;

        public void AddDye(IDye dye)
            {
            dyes.Add(dye);
            }

        public void Work()
            {

            if (GetType().Name == nameof(SleepyBunny))
                {
                Energy -= 15;
                }
            else
                {
                Energy -= 10;
                }

            if (Energy < 0)
                {
                energy = 0;
                }
            }
        }
    }
