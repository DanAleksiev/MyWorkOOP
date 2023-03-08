using Vehicles.IO;
using Vehicles.Models.interfaces;

namespace Vehicles.Models
    {
    public class Vehicle : IVehicle
        {
        private double AirconIncrease;

        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, double AirconIncrease)
            {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            TankCapacity = tankCapacity;
            this.AirconIncrease = AirconIncrease;
            }

        public double FuelQuantity { get; set; }

        public double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set; }

        public string Drive(double distance, bool isIncreasedConsumption = true)
            {
            double consumption = isIncreasedConsumption
            ? FuelConsumption + AirconIncrease
            : FuelConsumption;
            if (distance * consumption > FuelQuantity)
                {
                return ($"{this.GetType().Name} needs refueling");
                }
            FuelQuantity -= distance * consumption;
            return $"{this.GetType().Name} travelled {distance} km";
            }

        public virtual void Refuel(double amount)
            {
            if (amount <= 0)
                {
                throw new ArgumentException("Fuel must be a positive number");
                }

            if (amount + FuelQuantity > TankCapacity)
                {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
                }
            FuelQuantity += amount;
            }

        public override string ToString()
          => $"{this.GetType().Name}: {FuelQuantity:f2}";
        }
    }
