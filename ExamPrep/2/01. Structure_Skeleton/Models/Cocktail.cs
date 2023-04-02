using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChristmasPastryShop.Models
    {
    public abstract class Cocktail : ICocktail
        {
        private string name;
        private string size;
        private double price;

        protected Cocktail(string name, string size, double price)
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
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                    }
                name = value;
                }
            }

        public string Size
            {
            get => size;
            private set
                {
                size = value;
                }
            }

        public double Price
            {
            get => price;
            private set
                {
                if (size == "Large")
                    {
                    price = value;
                    }
                else if (size == "Middle")
                    {
                    price -= value / 3;
                    }
                else if (size == "Small")
                    {
                    price = value / 3;
                    }
                }
            }
        public override string ToString()
            {
            return $"{name} ({size}) - {price:f2} lv";
            }
        }
    }
