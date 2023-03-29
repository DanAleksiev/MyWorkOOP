using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
    {
    public abstract class MilitaryUnit : IMilitaryUnit
        {
        private int enduranceLevel;
        private double cost;

        protected MilitaryUnit(double cost)
            {
            this.EnduranceLevel = 1;
            this.Cost = cost;
            }

        public double Cost
            {
            get => this.cost;
            private set
                {
                this.cost = value;
                }
            }

        public int EnduranceLevel
            {
            get => this.enduranceLevel;
            private set
                {
                this.enduranceLevel = value;
                }
            }

        public void IncreaseEndurance()
            {
            this.enduranceLevel++;
            if (enduranceLevel > 20)
                {
                enduranceLevel = 20;
                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
                }
            }
        }
    }
