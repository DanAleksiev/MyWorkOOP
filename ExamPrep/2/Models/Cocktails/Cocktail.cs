using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _01.Structure2._0.Models.Cocktails
    {
    public abstract class Cocktail : ICocktail
        {
        private string name;
        private string size;
        private double price;
        private string[] posibleSizeValues = { "Small", "Middle", "Large" };

        public Cocktail(string name, string size, double price)
            {
            Name = name;
            Size = size;
            Price = price;
            }

        public string Name
            {
            get => name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentNullException(ExceptionMessages.NameNullOrWhitespace);
                    }
                name = value;
                }
            }

        //validate later!??!
        public string Size
            {
            get => size;
             private set
                {
                if (!posibleSizeValues.Contains(value))
                    {
                    size = null;
                    }
                size = value;
                }
            }

        public double Price
            {
            get => price;
            private set
                {
                if (this.size == "Large")
                    {
                    price = value;
                    }
                if (this.size == "Middle")
                    {
                    price = value - value / 3;
                    }
                if (this.size == "Small")
                    {
                    price = value / 3;
                    }
                }
            }

        public override string ToString()
            {
            return $"{this.name} ({this.size}) - {this.price:f2} lv";
            }
        }
    }
