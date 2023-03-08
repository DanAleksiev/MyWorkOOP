
namespace Vehicles.Models
    {
    internal class Car : Vehicle
        {
        private const double AirconIncrease = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity, AirconIncrease)
            {

            }
        }
    }
