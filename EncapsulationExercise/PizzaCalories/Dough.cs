using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
    {
    public class Dough
        {
        private const double BaseCalorie = 2;

        private int weight;
        private string flowerType;
        private string bakingTechnique;
        private double doughModifier;
        private double techniqueModifier;
        public Dough(string flowerType, string bakingTechnique,int weight)
            {

            FlowerType = flowerType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
            }
        public int Weight
            {
            get => weight;

            private set
                {
                if (value <1 || value > 200)
                    {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                    }
                weight = value;
                }
            }
        public string FlowerType
            {
            get => flowerType;
            private set
                {
                switch (value.ToLower())
                    {
                    case "white":
                    flowerType = value;
                    doughModifier = 1.5;
                    break;
                    case "wholegrain":
                    flowerType = value;
                    doughModifier = 1.0;
                    break;

                    default:
                    throw new ArgumentException("Invalid type of dough.");
                    }
                }
            }
        public string BakingTechnique
            {
            get => bakingTechnique;
            private set
                {
                switch (value.ToLower())
                    {
                    case "crispy":
                    bakingTechnique = value;
                    techniqueModifier = 0.9;
                    break;                   
                    case "chewy":
                    bakingTechnique = value;
                    techniqueModifier = 1.1;
                    break;
                    case "homemade":
                    bakingTechnique = value;
                    techniqueModifier = 1.0;
                    break;
                    
                    default:
                    throw new ArgumentException("Invalid type of dough.");
                    }
                }
            }
        public double Calories { get; private set; }

        public double CaloriesCalculaterOfDough()
            {
            Calories = (BaseCalorie * Weight) * doughModifier * techniqueModifier;
            return Calories;
            }
        }
    }
