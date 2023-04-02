using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
    {
    public abstract class FormulaOneCar : IFormulaOneCar
        {
        private string f1Car;
        private int horsepower;
        private double engineDisplacement;

        public FormulaOneCar(string model, int horsepower, double engineDisplacement)
            {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
            }

        public string Model
            {
            get => f1Car;
            private set
                {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1CarModel, value));
                    }
                f1Car = value;
                }
            }

        public int Horsepower
            {
            get => horsepower;
            private set
                {
                if (value < 900 || value > 1050)
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1HorsePower, value));
                    }
                horsepower = value;
                }
            }

        public double EngineDisplacement
            {
            get => engineDisplacement;
            private set
                {
                if (value < 1.6 || value > 2.00)
                    {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1EngineDisplacement, value));
                    }
                engineDisplacement = value;
                }
            }

        public double RaceScoreCalculator(int laps)
            {
            return engineDisplacement / horsepower * laps;
            }
        }
    }
