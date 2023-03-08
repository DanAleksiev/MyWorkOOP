using Vehicles.Models.interfaces;

namespace Vehicles.Factories.interfaces
{
    public interface IVehicleFactory
        {
        IVehicle Create(string type, double fuelQuantity, double fuelConsumption,double tankCapacity);
        }
    }