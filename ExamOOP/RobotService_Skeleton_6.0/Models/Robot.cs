using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
    {
    public abstract class Robot : IRobot
        {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private readonly List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
            {
            Model = model;
            BatteryCapacity = batteryCapacity;
            ConvertionCapacityIndex = convertionCapacityIndex;

            interfaceStandards = new List<int>();
            batteryLevel = batteryCapacity;
            }

        public string Model
            {
            get => model;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                    }
                model = value;
                }
            }

        public int BatteryCapacity
            {
            get => batteryCapacity;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                    }
                batteryCapacity = value;
                }
            }

        public int ConvertionCapacityIndex
            {
            get;
            private set;
            }

        public int BatteryLevel
            {
            get => batteryLevel;
            private set => batteryLevel = value;
            }

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards;

        public void Eating(int minutes)
            {
            batteryLevel = ConvertionCapacityIndex * minutes;
            if (batteryLevel > batteryCapacity)
                {
                batteryLevel = batteryCapacity;
                }
            }

        public bool ExecuteService(int consumedEnergy)
            {
            if (consumedEnergy <= batteryLevel)
                {
                batteryLevel -= consumedEnergy;
                return true;
                }
            else
                {
                return false;
                }
            }

        public void InstallSupplement(ISupplement supplement)
            {
            interfaceStandards.Add(supplement.InterfaceStandard);
            batteryLevel -= supplement.BatteryUsage;
            batteryCapacity -= supplement.BatteryUsage;
            }

        public override string ToString()
            {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.Append($"--Supplements installed: ");
            if (interfaceStandards.Count == 0)
                {
                sb.AppendLine($"none");
                }
            else
                {
                sb.AppendLine(string.Join(" ", interfaceStandards));
                }

            return sb.ToString().Trim();
            }
        }
    }
