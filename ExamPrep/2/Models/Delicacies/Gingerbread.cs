using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.Structure2._0.Models.Delicacies
    {
    public class Gingerbread : Delicacy
        {
        private const double gingerbreadPrice = 4.00;
        public Gingerbread(string name) : base(name, gingerbreadPrice)
            {
            }
        }
    }
