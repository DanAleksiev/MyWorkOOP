namespace Vehicles.Models.interfaces
{
    public interface IVehicle
    {

        public double FuelQuantity { get; }
        public double FuelConsumption { get; }
        public double TankCapacity { get; }
        string Drive(double distance,bool isIncreasedConsumption = true);
        void Refuel(double amount);
    }
}
