using _01.Structure2._0.Models.Cocktails;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
    {
    public class MulledWine : Cocktail
        {
        private const double priceMulledWine = 13.50;
        public MulledWine(string name, string size) : base(name, size, priceMulledWine)
            {
            }
        }
    }
