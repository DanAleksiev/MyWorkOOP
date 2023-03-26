using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.Structure2._0.Models.Delicacies
    {
    public class Stolen : Delicacy
        {
        private const double stolenPrice = 3.50;
        public Stolen(string name) : base(name, stolenPrice)
            {
            }
        }
    }
