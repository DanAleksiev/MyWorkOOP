using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
    {
    internal class Truck : Vehicle
        {
        private const double AirconIncrease = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity, AirconIncrease)
            {

            }

        public override void Refuel(double amount)
            {
            if (amount + FuelQuantity > TankCapacity)
                {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
                }
            base.Refuel(amount * 0.95);
            }
        }
    }
