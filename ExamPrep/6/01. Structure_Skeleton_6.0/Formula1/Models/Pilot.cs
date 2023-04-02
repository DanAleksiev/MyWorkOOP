using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
    {
    public class Pilot : IPilot
        {
        private string fullName;
        private bool canRace;
        private IFormulaOneCar car;
        private int numberOfWins;

        public Pilot(string fullName)
            {
            FullName = fullName;
            }

        public string FullName
            {
            get => fullName;
            private set
                {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                    }
                fullName = value;
                }
            }

        public bool CanRace => canRace;

        public IFormulaOneCar Car
            {
            get => car;
            private set
                {
                if (value == null)
                    {
                    throw new NullReferenceException(string.Format(ExceptionMessages.InvalidCarForPilot));
                    }
                car = value;
                }
            }

        public int NumberOfWins => numberOfWins;


        public void AddCar(IFormulaOneCar car)
            {
            Car = car;
            canRace = true;
            }

        public void WinRace()
            {
            numberOfWins++;
            }

        public override string ToString()
            {
            return $"Pilot {fullName} has {numberOfWins} wins.";
            }
        }
    }
