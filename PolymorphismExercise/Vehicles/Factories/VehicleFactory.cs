using System.Security.Cryptography.X509Certificates;
using Vehicles.Factories.interfaces;
using Vehicles.Models;
using Vehicles.Models.interfaces;

namespace Vehicles.Factories
    {
    public class VehicleFactory : IVehicleFactory
        {
        public IVehicle Create(string type, double fuelQuantity, double fuelConsumption, double tankCapacity)
            {
            if (fuelQuantity > tankCapacity)
                {
                fuelQuantity = 0;
                }
            switch (type)
                {
                case "Car":
                return new Car(fuelQuantity, fuelConsumption, tankCapacity);
                case "Truck":
                return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
                case "Bus":
                return new Bus(fuelQuantity, fuelConsumption,tankCapacity);
                default:
                throw new ArgumentException("Invalid vehicle type");
                }
            }
        }
    }
