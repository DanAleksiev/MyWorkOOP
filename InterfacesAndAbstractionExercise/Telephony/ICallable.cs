using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
    {
    internal interface ICallable
        {
        public string Number { get; }
        public bool ValidateNumber();
        public void Dial();
        }
    }
