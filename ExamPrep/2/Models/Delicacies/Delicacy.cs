using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace _01.Structure2._0.Models.Delicacies
    {
    public abstract class Delicacy : IDelicacy
        {
        private string name;
        private double price;

        protected Delicacy(string name, double price)
            {
            Name = name;
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

        public double Price
            {
            get => price;
            private set => price = value;
            }

        public override string ToString()
            {
            return $"{Name} - {Price:f2} lv";
            }
        }
    }
